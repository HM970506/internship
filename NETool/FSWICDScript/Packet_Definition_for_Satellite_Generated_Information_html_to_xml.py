#!/usr/bin/env python
# coding: utf-8

# In[1]:


from bs4 import BeautifulSoup
import pandas as pd
from html_table_parser import parser_functions as parser
from xml.etree.ElementTree import Element, dump
from xml.etree.ElementTree import ElementTree


# In[2]:


#FSWOperationInformation.xml 

##read html
html_FSWO=open("C:/Users/user/Desktop/FSWICD/FSWOPInfo/[ESGS] FSW Operation Information - SX FSW - Wiki SI.html", 'r', encoding='UTF8')
soup=BeautifulSoup(html_FSWO, 'html.parser')


# In[3]:


#html to string -> 각 table별 개수 파악
count_list=[]

split_str="<div class=\"conf-macro output-block\" data-hasbody=\"true\" data-macro-name=\"numberedheadings\">"
h3_list=str(soup).split(split_str)[1].split("<h3 ")

del h3_list[0]

for now in h3_list:
    ## th가 2개 들어 있으므로, 2로 한번 더 나눠줌
    count_list.append(int(now.count("</td>")/(now.count("</th>")/2)))
    
del count_list[0]
print(count_list)


# In[4]:


##html parsing -> xml용 dataframe 제작
tr=soup.find_all("tr")
td_list=[]
th_list=[]

for now in tr:
    th_find=now.find_all("th")
    td_find=now.find_all("td")
    if(th_find):
        th_list.append(th_find)
    if(td_find):
        td_list.append(td_find)


# In[5]:


##필요한 부분까지 자르기

while(th_list):
    now_list=th_list[0]
    if(now_list[0].get_text()=='Start Address'):
        break
    del th_list[0]

while(td_list):
    now_list=td_list[0]
    if(now_list[0].get_text()=='D0'):
        break
    del td_list[0]


# In[6]:


##Make Packet Format

Paket_column=[]
Paket_value=[]

for data in th_list[0]:
    Paket_column.append(data.get_text())
    
del th_list[0];
del th_list[0];

while(1):
    Paket_value_list=[];
    for data in td_list[0]:
        Paket_value_list.append(data.get_text())
    Paket_value.append(Paket_value_list)
    
    ##표 끝값이 나오면 정지
    if(len(Paket_value)==count_list[0]):
        del td_list[0]
        del count_list[0]
        break
    del td_list[0]
    
Paket=pd.DataFrame(columns=Paket_column)

for now in Paket_value:
    Paket.loc[len(Paket)]=now
Paket


# In[7]:


##Make Parameter Definition

Parameter_column=[]
Parameter_value=[]

for data in th_list[0]:
    if(th_list):
        Parameter_column.append(data.get_text())
    
del th_list[0];
del th_list[0];

while(td_list):
    Parameter_value_list=[]
    Symbolic_list=[]
    
    for data in td_list[0]:
        ##Symbolic definition은 여러 값을 가지므로, list로 개별 처리
        if(len(Parameter_value_list)<=8):
            Parameter_value_list.append(data.get_text())
        else:
            if(data.find_all('li')):
                for symbolic_value in data.find_all('li'):
                    Symbolic_list.append(symbolic_value.get_text())
                                     
    Parameter_value_list.append(", ".join(map(str, Symbolic_list)))
    Parameter_value.append(Parameter_value_list)
    
    ##표 끝값이 나오면 break
    if(len(Parameter_value)==count_list[0]):
        del count_list[0]
        break
    del td_list[0]
    
Parameter=pd.DataFrame(columns=Parameter_column)
for now in Parameter_value:
    Parameter.loc[len(Parameter)]=now
Parameter


# In[8]:


##merging
merge=pd.merge(Paket, Parameter, how='left', on=None)
merge


# In[9]:


##make datacolumn
column={'Type':[], 'Len':[], 'Unit':[], 'FieldName':[], 'NormalRange':[], 'Description':[], 'ValueMean':[]}
table_FSWO=pd.DataFrame(column)
table_FSWO['Type']=merge['Data Type']
table_FSWO['Len']=merge['Length(bit)']
table_FSWO['Unit']=merge['Unit']
table_FSWO['FieldName']=merge['Name']
table_FSWO['NormalRange']=merge['Normal Range']
table_FSWO['Description']=merge['Description']
table_FSWO['ValueMean']=merge['Symbolic Definition']
table_FSWO


# In[10]:


##dataframe 값 양식에 맞게 수정

for i in table_FSWO.index:
    now= table_FSWO.iloc[i].copy()
    now['Type']=now['Type'].replace('\n','')
    if('BYTE' in now['Type']):
        now['Type']='Byte'
    if('FLOAT' in now['Type']):
        now['Type']='Single'
    now['Type']="System."+now['Type']
    now['Len']=int(int(now['Len'])/8)
    now['NormalRange']=str(now['NormalRange']).replace('\n','')
    now_NormalRange_list=now['NormalRange'].split()
    if(len(now_NormalRange_list)>1):
        now['NormalRange']=now_NormalRange_list[0]+";"+now_NormalRange_list[2]
    ##수정값 반영
    table_FSWO.iloc[i]=now

table_FSWO


# In[16]:


##dataframe to xml
root_node=Element("FSWOperationInformation")


for i in table_FSWO.index:
        
    now= table_FSWO.iloc[i].copy()
    parent_node=Element("AlphanumericInfo")
    
    value_node=Element("Value");
    value_node.text="0";
    parent_node.append(value_node)
    
    for x in table_FSWO.columns:
        if(x=="ValueMean"):
            if(now[x]):
                for list_now in str(now[x]).split(', '):
                    valuemean_node=Element(x)
                    value_node=Element("Value")
                    mean_node=Element("Mean")
                    
                    list_now=list_now.replace(u'\xa0',' ')
                    list_ = list_now.split(' - ')
                    if('x' in list_[0]):
                        value_node.text=str(int(list_[0], 16))
                    else:
                        value_node.text=list_[0]
                    
                    if(len(list_)>1):
                        mean_node.text=list_[1]
                
                    valuemean_node.append(value_node)
                    valuemean_node.append(mean_node)
                
                    parent_node.append(valuemean_node)
        
        else:
            node=Element(x)
            if(str(now[x])):
                node.text=str(now[x]).replace(u'&#160;', ' ')
            else: 
                node.text=""
            parent_node.append(node)
   
    root_node.append(parent_node)

    
##xml 여백 추가
def indent(elem, level=0):
    i = "\n" + level*"  "
    if len(elem):
        if not elem.text or not elem.text.strip():
            elem.text = i + "  "
        if not elem.tail or not elem.tail.strip():
            elem.tail = i
        for elem in elem:
            indent(elem, level+1)
        if not elem.tail or not elem.tail.strip():
            elem.tail = i
    else:
        if level and (not elem.tail or not elem.tail.strip()):
            elem.tail = i

indent(root_node)
dump(root_node)


# In[ ]:


##저장
ElementTree(root_node).write("./FSWOperationInformation.xml")


# In[ ]:





# In[ ]:




