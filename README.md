## Step -1- Report
### Rules:
  - (1) Kullanıcı, raporunu almak istediği klasörü dosya sistemi üzerinden seçebiliyor olacak.
  - (2) Kullanıcı, dosyaları hangi tarihe göre sınıflandırma yapmak istediğini seçebiliyor olacak. Seçebileceği tarihler : creation date, modification date, access date.
  - (3) Kullanıcı, sınıflandırma tarihini takvim ekranı üzerinden seçebiliyor olacak.
  - (4) Kullanıcı, hangi formatta rapor alacağını seçebiliyor olacak (Excel, Txt)
  - (5) O an hangi dosyanın taranıyor olduğu arayüzde görülebiliyor olacak.
  - (6) Taramanın kaç dakikada tamamlandığı görüntülenebiliyor olacak.
#### Rule-1 Descriptions:
  - (1) => FileSystemReporterForm class <b>BrowseButton_Click</b> method. Used FolderBrowser Class
  - (1 cont.) => For Target Path, used SaveFileDialog class. Method name is <b> browseTargetButton_Click </b>
  - (2) => For Filter by date operations, you can look at the <b> FileReporterSystemApp </b> class that use the <b> FilterService </b> and another classes writed for step 2.
  - (3) => User can choose the date on the GUI.
  - (4) => User can decide the which want formatted report on the browse button near the "Target Path" TextBox and in code you can look at the <b> browseTargetButton_Click </b> method. Supported Formats are (.txt) and (.xlsx) but not implemented yet.
  - (5, 6) => User can look at the bottom on the GUI for seeing the which file scanned and finish time. 
