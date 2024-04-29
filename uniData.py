import requests
from bs4 import BeautifulSoup
import re
url = "https://yokatlas.yok.gov.tr/universite.php"
response = requests.get(url, verify=False)
#response = requests.get(url)
soup = BeautifulSoup(response.text, 'html.parser')

# "unilist" sınıfına sahip olan <li> öğelerini bulma
unilist_items = soup.find_all('li', class_='unilist')
counter=1
print("INSERT INTO uni (id,name,location,type,url) ") 
for university in unilist_items:
    # Üniversite adını alın
    id=counter
    uni_ad = university.find("h3", class_="baslik").text.strip()
    
    # Tür bilgisini alın
    tur = university.find("span", class_="tur").text.strip()
    
    # Şehir bilgisini alın
    sehir = university.find("span", class_="sehir").text.strip()
    
    # URL'yi alın
    url = university.find("a",target="_blank").text.strip()
  
   
    print("VALUES  (", str(counter), ",","'"+uni_ad+"'",",","'"+sehir+"'",",","'"+tur+"'",",","'"+url+"'",");")
    # Bilgileri ekrana yazdırın
    #print("Üniversite Adı:", uni_ad)
    #print("Tür:", tur)
    #print("Şehir:", sehir)
    #print("URL:", url)
    #print("--------------------------")
    counter=counter+1