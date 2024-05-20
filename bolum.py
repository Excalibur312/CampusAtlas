import pandas as pd
import requests
from bs4 import BeautifulSoup

# Excel dosyasını oku
file_path = '/Users/esmanurarslan/Library/Mobile Documents/com~apple~CloudDocs/6th term/CampusAtlas-master/lisans.xls'
df = pd.read_excel(file_path, header=None)  # Başlık satırı olmadan oku

# DataFrame'in ilk birkaç satırını kontrol et
print("DataFrame'in ilk birkaç satırı:")
print(df.head())

# A sütunundaki verileri 2. satırdan itibaren al
program_ids = df.iloc[1:, 0].dropna().astype(str)  # İlk sütun ve 2. satırdan itibaren

# Verileri saklamak için boş bir liste oluştur
data = []

# Her program ID'si için ilgili sayfaya gidip bilgileri çıkar
for program_id in program_ids:
    url = f"https://yokatlas.yok.gov.tr/lisans.php?y={program_id}"
    try:
        response = requests.get(url, verify=False)  # verify=False SSL doğrulamasını devre dışı bırakır
        response.raise_for_status()
        soup = BeautifulSoup(response.content, 'html.parser')

        # Üniversite adı
        uni_name_tag = soup.find('div', class_='panel-heading').find('h3', class_='panel-title pull-left')
        uni_name = uni_name_tag.text.strip() if uni_name_tag else 'N/A'

        # Fakülte adı
        faculty_tag = soup.find('h3', class_='panel-title pull-left', string=lambda text: text and "Fakülte / YO :" in text)
        faculty = faculty_tag.text.split('Fakülte / YO :')[-1].strip() if faculty_tag else 'N/A'

        # Bölüm adı
        program_tag = soup.find('h2', class_='panel-title pull-left')
        program_name = program_tag.text.split('-')[-1].strip() if program_tag else 'N/A'

        # Verileri listeye ekle
        data.append({
            'Program ID': program_id,
            'Üniversite Adı': uni_name,
            'Fakülte': faculty,
            'Bölüm Adı': program_name
        })
    except requests.exceptions.RequestException as e:
        print(f"Error fetching data for Program ID {program_id}: {e}")

# Çıkarılan verileri DataFrame'e dönüştür
result_df = pd.DataFrame(data)

# Sonuçları yeni bir Excel dosyasına kaydet
result_df.to_excel('program_details.xlsx', index=False)
