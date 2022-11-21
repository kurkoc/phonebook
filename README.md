# phonebook

Bu uygulama contact ve report olmak üzere iki adet servisten oluşmaktadır. Servislere yapılacak yönlenmeleri sağlamak için iki servisin önünde bir api gateway kullanılmaktadır. api gateway olarak ocelot kullanılmıştır.

![phonebook_schema](https://user-images.githubusercontent.com/6065955/203173396-80d8073b-d5e2-4215-92ce-5f4f8397ead8.PNG)


Contact microservisi; 
  - .net core 6 web api
  - postgresql

teknolojileri kullanarak geliştirimiştir. Veri bağlantısını sağlamak için Entity Framework kullanılmıştır.  

Geliştirme paradigması olarak Domain Driven Design tercih edilmiştir. Fluent Validation, Automapper gibi yardımcı kütüphaneler kullanılmıştır.

Report microservisi; 
  - .net core 6 web api
  - postgresql
  - rabbitmq

teknolojileri kullanarak geliştirimiştir. Veri bağlantısını sağlamak için Entity Framework kullanılmıştır.

Uygulamayı çalıştırmak için 
  ```ruby
  docker compose -f docker-compose.yml -f docker-compose.override.yml up -d
  ```
 komutu kullanılabilir. (ufak bir eksiklik var tamamlanacak)






