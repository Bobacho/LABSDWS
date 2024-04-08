CREATE DATABASE DbCatalogo;
GO
USE DbCatalogo;

create table Usuario(
	IdUsuario int not null primary key,
	CodUsuario varchar(50) not null,
	Clave varchar(50) not null,
	Nombres varchar(50) not null,
	IdRol int
);
create table Stock(
	IdProducto int not null primary key,
	StockItems int not null,
	PuntoRepo int not null,
	PrecioVenta decimal(18,2) null
);
create table Rol(
	IdRol int not null primary key,
	DesRol varchar(80) not null
);
create table Producto(
	IdProducto int not null primary key,
	Nombre varchar(50) not null,
	IdCategoria int,
	Descripcion varchar(100) not null
);
create table Categoria(
	IdCategoria int not null primary key,
	DescCategoria varchar(100) not null
);
GO
alter table Usuario add constraint FK_IdRol foreign key (IdRol) references Rol(IdRol);

alter table Stock add constraint FK_IdProducto foreign key (IdProducto) references Producto(IdProducto);

alter table Producto add constraint FK_IdCategoria foreign key (IdCategoria) references Categoria(IdCategoria);

GO

