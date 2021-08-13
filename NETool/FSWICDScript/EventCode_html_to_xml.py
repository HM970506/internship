#!/usr/bin/env python
# coding: utf-8

# In[1]:


from bs4 import BeautifulSoup
import pandas as pd
from html_table_parser import parser_functions as parser
from xml.etree.ElementTree import Element, dump
from xml.etree.ElementTree import ElementTree
pd.set_option('display.max_columns',None)
pd.set_option('display.max_rows',None)


# In[2]:


##Errorcode html
html_Errorcode=open("C:/Users/user/Desktop/FSWICD/EventCode/[ESGS] OST Task Status List - SX FSW - Wiki SI.html", 'r', encoding='UTF8')
soup=BeautifulSoup(html_Errorcode, 'html.parser')


# In[3]:


##Count dataframe

split_str="<div class=\"conf-macro output-block\" data-hasbody=\"true\" data-macro-name=\"numberedheadings\">"
    
count_list=[]
if(split_str in str(soup)):
    print()
h2_list=str(soup).split(split_str)[1].split("<h2 ")


def isNode(now):    

    if("Code Definition" in now): return True
    if("Format" in now): return True
    if("Parameter Definition" in now): return True
    return False


h3_list=[]
for now in h2_list:
    now_list=now.split("<h3")
    h3_list.append(now_list)

for now_list in h3_list:
    for now in now_list:
            if(isNode(now)):
                if(now.count("<th")==0):
                    count_list.append(0)
                else:
                    count_list.append(int(now.count("</td>")/4))
    
print(count_list)


# In[4]:


##Get Description

h3_list=str(soup).split(split_str)[1].split("<h3 ")
del h3_list[0]

p_list_str=[]

for h3 in h3_list:
    if("Description" in h3):
        if("</h3><p>" in h3):
            p_list_str.append((h3[h3.find("</h3><p>"):h3.find("</p>")]).replace("</h3><p>", ""))

p_list_str


# In[5]:


##html parsing
tr=soup.find_all("tr")

td_list=[]
th_list=[]

for now in tr:
    th_find=now.find_all("th")
    td_find=now.find_all("td")
    if(th_find):
        th_now_list=[]
        for th in th_find:
            th_now_list.append(th.get_text())
        th_list.append(th_now_list)
    if(td_find):
        td_now_list=[]
        for td in td_find:
            if(len(td_now_list)>3):
                sd_list=[]
                for sd in td.find_all("li"):
                    sd_list.append(sd.get_text().replace(u'\xa0', ''))
                td_now_list.append(','.join(sd_list))
            else:
                td_now_list.append(td.get_text().replace(u'\xa0', ''))
        td_list.append(td_now_list)
        
td_list


# In[6]:


##Make Dictionary

##여기가 문제다. 여기서 OST_STATUS_FAIL_GET_MSGQ_DATA 가 사라짐!
dataframe_dic={}

dataframe_columns=[['Task', 'Code', 'Mnemonic', 'Level'],
 ['Start Address', 'Length(bit)', 'Name', 'Data Type'],
 ['Name', 'Description', 'Parameter Type', 'Symbolic Definition']]

td_list_=td_list.copy()
p_list_str_=p_list_str.copy()
dataframe_list=[]

for index in range(0, len(p_list_str)*3):
    column=dataframe_columns[index%3]
        
    dataframe=pd.DataFrame(columns=column)
        
     ##countlist에 등록한 수만큼 행을 가질 때까지 while문을 돎
    while(td_list_):
        if not(len(dataframe)==count_list[index]):
            dataframe.loc[len(dataframe)]=td_list_[0]
            del td_list_[0]
        else:
            break
                
    dataframe_list.append(dataframe)
    
    if(len(dataframe_list)==3):
        dataframe_dic[p_list_str_[0]+"/"+str(int(index/3))]=(dataframe_list)
        del p_list_str_[0]
        dataframe_list=[]


# In[7]:


##빈 테이블 널처리 해줘야 해.;....


# In[8]:


##merge
column=["CodeName", "CodeValue", "Description", "LogLevel", "DataType", "DataDef"]
table_errorcode=pd.DataFrame(columns=column)
x=0
test=[]
def get(now):    
    return (",".join(now.values)).replace("[", "").replace("]","")

for name, value in dataframe_dic.items():

    this_row=[get(value[0]['Mnemonic']), str(int(get(value[0]["Code"]), 16)), name.split("/")[0],
              get(value[0]["Level"]), get(value[1]["Data Type"]) , get(value[1]["Name"])]
    table_errorcode.loc[x]=this_row
    x=x+1


# In[9]:


table_errorcode


# In[10]:


##dataframe to xml
root_node=Element("EventCodeCollection")
task_node=Element("TaskName")
code_node=Element("Codes")

for i in table_errorcode.index:
        
    now= table_errorcode.iloc[i].copy()
    parent_node=Element("EventCodeDesc")
    
    for value in table_errorcode.columns:
        child_node=Element(value)
        child_node.text=table_errorcode[value][i]
        parent_node.append(child_node)
    code_node.append(parent_node)
    
a_list=soup.find_all("a")
title=""

for now in a_list:
    if("[ESGS]" in now.get_text()):
        title=now.get_text().split(" ")[1]

task_node.text=title
root_node.append(task_node)
root_node.append(code_node)
    
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


# In[11]:


##저장
ElementTree(root_node).write("./"+title+"Code.xml")


# In[12]:


title


# In[ ]:




