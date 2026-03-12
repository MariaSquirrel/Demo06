
create table demo06.user
(
id Serial primary key,
name varchar(70), 
family varchar(70), 
patronomic varchar(70), 
login varchar(70), 
password varchar(70),
role_id int references demo06.Role(id)
);

create table demo06.client
(
id Serial primary key,
name varchar(70), 
family varchar(70), 
patronomic varchar(70), 
email varchar(70),
phone varchar (20)
);

create table demo06.services
(
id Serial primary key,
name varchar(70), 
cost decimal(10,2)
);

create table demo06.orders
(
id Serial primary key,
DateCreate Date, 
container_id int references demo06.container(id),
material_id int references demo06.material(id),
user_id int references demo06.user(id)
);

create table demo06.Role
(
id Serial primary key,
name varchar(70)
);

create table demo06.container
(
id Serial primary key,
name varchar(70)
);

create table demo06.material
(
id Serial primary key,
name varchar(70)
);

-- Таблица связи заказа с клиентами
create table demo06.order_client
(
order_id int references demo06.orders(id) ON DELETE CASCADE, 
client_id int references demo06.client(id) ON DELETE CASCADE,  
primary key (order_id, client_id)
);

-- Таблица связи заказа с услугами
create table demo06.order_services
(
order_id int references demo06.orders(id) ON DELETE CASCADE,  
service_id int references demo06.services(id) ON DELETE CASCADE, 
primary key (order_id, service_id)
);

