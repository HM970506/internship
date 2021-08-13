#!/usr/bin/env python
# coding: utf-8

# In[1]:


from bs4 import BeautifulSoup
import pandas as pd
import json
from collections import OrderedDict
from html_table_parser import parser_functions as parser


# In[2]:


##Task id html
html_GSTriggered=open("C:/Users/user/Desktop/FSWICD/TaskID/[ESGS] TMTC Definitions of Between S_C and MCS - SX FSW - Wiki SI.html", 'r', encoding='UTF8')
soup=BeautifulSoup(html_GSTriggered, 'html.parser')

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
            td_now_list.append(td.get_text().replace(u'\xa0', ''))
        td_list.append(td_now_list)
        
td_list


# In[3]:


##Make TaskID DataFrame
taskid_value=[]
taskid_column=[]

taskid_column=th_list[0]
del th_list[0]

taskid_value=td_list

taskid=pd.DataFrame(columns=taskid_column)
for now in taskid_value:
    taskid.loc[len(taskid)]=now
taskid


# In[4]:


##Make Dataframe dictionary 준비
html_link="C:/Users/user/Desktop/FSWICD/TC/[ESGS] ADS(Active Device Selection) Management - SX FSW - Wiki SI.html"
dataframe_dic={}
split_str="<div class=\"conf-macro output-block\" data-hasbody=\"true\" data-macro-name=\"numberedheadings\">"
th_list=["Properties", "Packet Format", "Parameter Definition"]

def isNode(now):    

    if("Properties" in now): return ["TC/TM Type", "Sub Packet ID", "Formatter Name"]
    if("Packet Format" in now): return ["Start Address", "Length(bit)", "Name", "Data Type", "Default Value"]
    if("Parameter Definition" in now): return ["Name", "Description", "Parameter Type", "TM/TC Type", "Data Conversion", "Unit", "Category", "Normal Range", "Warning Range", "Symbolic Definition"]
    return -1
    


# In[5]:


#Make count list -> 각 데이터프레임의 행 개수

split_str="<div class=\"conf-macro output-block\" data-hasbody=\"true\" data-macro-name=\"numberedheadings\">"
html_GSTriggered=open(html_link, 'r', encoding='UTF8')
soup=BeautifulSoup(html_GSTriggered, 'html.parser')
    

count_list=[]
h2_list=str(soup).split(split_str)[1].split("<h2 ")

h3_list=[]
for now in h2_list:
    if not ("Structure Format" in now):
        now_list=now.split("<h3")
        h3_list.append(now_list)

for now_list in h3_list:
    for now in now_list:
        now_column=isNode(now)
        if not(now_column==-1):
            count_list.append(int(now.count("</td>")/len(now_column)))
    
print(count_list)
print(len(count_list))


# In[6]:


#Make title Dictionary -> structure 제외

h1_list=soup.find_all("h1")
h2_list=soup.find_all("h2")
title_dic={}

for h1 in h1_list:
    if(". " in h1.get_text()):
        now_h1=h1.get_text().split(". ")
        if not('Structure' in now_h1[1]):
            title_dic[now_h1[1]]=[]
            for h2 in h2_list:
                now_h2=h2.get_text().split(".")
                if(now_h1[0]==now_h2[0]):
                    child_dic={}
                    child_dic[now_h2[2].lstrip()]=[]
                    title_dic[now_h1[1]].append(child_dic)
                
title_dic


# In[7]:


##html parsing
tr=soup.find_all("tr")
sp=soup.find_all("h1")

td_list=[]
h1_list=[]

for now in tr:
    td_find=now.find_all("td")
    if(td_find):
        td_now_list=[]
        for td in td_find:
            if(len(td_now_list)>8):
                sd_list=[]
                for sd in td.find_all("li"):
                    sd_list.append(sd.get_text().replace(u'\xa0', ''))
                td_now_list.append(', '.join(sd_list))

            else:
                if(td.get_text()):
                    td_now_list.append(td.get_text().replace(u'\xa0', ''))
                else:
                    td_now_list.append("")
        td_list.append(td_now_list)
        
td_list


# In[19]:


##remark 부분 삭제

#del_index=count_list[0]+count_list[1]

#print(td_list)

#for i in range(0,count_list[2]):
#    del td_list[del_index+i][len(td_list[del_index+i])-1]
 


# In[9]:


##count에 맞추어 빈 테이블 존재 구간 삭제 밑준비

def list_chunk(lst, n):
    return [lst[i:i+n] for i in range(0, len(lst), n)]


td_list_count=[]
td_list_=td_list.copy()

###각 count를 3개씩 잘라서, td_list를 이에 맞게 나눈다
list_3=list_chunk(count_list, 3)

for now_list in list_3:
    parent_list=[]
    for row_count in now_list:
        child_list=[]
        for i in range(0, row_count):
            if(td_list_):
                child_list.append(td_list_[0])
                del td_list_[0]
        parent_list.append(child_list)
    td_list_count.append(parent_list)


# In[10]:


###해당 list를 title dictionary와 합침.

td_list_count_=td_list_count.copy()



for outkey, outvalue in title_dic.items():
    for list_value in outvalue:
        for inkey, invalue in list_value.items():
            if(td_list_count_):
                list_value[inkey]=td_list_count_[0]
                del td_list_count_[0]

title_dic


# In[11]:


##빈 리스트를 가지는 모든 딕셔너리 삭제

del_list=[]

empty_list=[]
empty_dic={}

def list_dic_index(dic_list, key):
    for now in dic_list:
        if(key in now):
            return dic_list.index(now)
    return -1
            


###루프중 삭제가 불가능하므로 빈 키 이름을 모음
for outkey, outvalue in title_dic.items():
    for list_value in outvalue:
        for inkey, invalue in list_value.items():
            if(empty_list in invalue):
                del_list.append([outkey, inkey])

###삭제
for del_key in del_list:
    index=list_dic_index(title_dic[del_key[0]], del_key[1])
    del title_dic[del_key[0]][index]
    if not(title_dic[del_key[0]]):
        del title_dic[del_key[0]]
            
del_list


# In[12]:


title_dic


# In[17]:


##json making

json_dic={}

for outkey, outvalue in title_dic.items():
    tc_list=[]
    tc_list.append({"Packet Type":outkey})
    
    for list_value in outvalue:
        for inkey, invalue in list_value.items():
            json_dic[inkey]={}
            tc_list.append({"Subpacket Type":inkey})
            tc_list.append({"Task ID":   str(taskid.loc[taskid["Command Name"]==inkey]["FSW Task"].values).split("'")[1]   })
            symbolicdef_list=[]
 
            data_list=[]
            for row in invalue[2]:##Parameter Definition
                symbolicdef_row_list=row[len(row)-1].split(", ")
                       
                for now in symbolicdef_row_list:
                    symbolicdef_row_list[symbolicdef_row_list.index(now)]=now.split("-")[1].lstrip()
                
                symbolicdef_list.append({row[0]:symbolicdef_row_list})
                data_list.append( {row[0]: "{"+str(invalue[2].index(row))+"}" })
                
            if(symbolicdef_list):
                json_dic[inkey]["SymbolicDef"]={}
                for now in symbolicdef_list:
                    json_dic[inkey]["SymbolicDef"].update(now)
            
            json_dic[inkey]["TC"]={}
            
            for now in tc_list:
                json_dic[inkey]["TC"].update(now)
                
                
            json_dic[inkey]["TC"]["Data"]={}
            for now in data_list:
                json_dic[inkey]["TC"]["Data"].update(now)
                
            
            
json_dic


# In[14]:


with open("ESGSTCFormat.json", "w") as f:
    json.dump(json_dic, f, ensure_ascii=False, indent=4)


# In[ ]:





# In[ ]:





# In[ ]:





# In[ ]:




