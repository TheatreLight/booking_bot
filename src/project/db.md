Диаграмма БД
============

![Project](misc/ImagesProject/Db_deagramReal.png)



Составляющие БД
---------------
1. School21 - таблица с id(int) и названием кампуса CampusName(char(100)).
2. Campus - таблица с внешним ключем от CampusId(int) к id(int) таблицы School21,
с внешним ключем от UserId(int)NULL к id(int) таблицы User, с внешним ключем от ObjectId(int)NULL к ObjectId(int) таблицы Object.
3. User - таблица с данными пользователя id(int), CampusId(int), UserName(char(100))NULL, UserLogin(char(100)), Role(int).
4. Booking - таблица с внешним ключем от UserId(int) к id(int) таблицы User, с внешним ключем от UserId(int) к ObjectId(int) таблицы Object, Date(date), TimeFrom(time(7)), TimeTo(time(7)). 
5. Object - таблица с данными бронируемого объекта  ObjectId(int), NameObject(char(100)), Info(text)NULL, Level(int)NULL, NumberRoom(int)NULL, Type(int).
