# Integrantes
#### *✅ Yeison Mauricio Álvarez Herrera*
#### *✅ Katherin de Jesus Martinez Rivas*
#### *✅ Kimberly Sujey Flores Prudencio*
#### *✅ Belquiz Tatiana Cruz Martínez*
#### *✅ Jhonatan Ernesto Henriquez Guevara*

# Idea
**Nuestro compañero Yeison Mauricio va emprender un negocio, el cual es la venta de todo tipo de hardware computacional, por lo cual como grupo nos hemos dado la tarea de desarrollar un sistema a la medida.**

**Sera un sistema administrativo para su negocio, el cual contara con distintos modulos entre ellos:**

**Modulo Login, Ventas, Compras e Inventario**

**En caso de que se completen satisfactoriamente todos los modulos prioritarios y se cumpla con los objetivos principales del proyecto, se podría evaluar la posibilidad de incluir funcionalidades adicionales en el sistema como por ejemplo un modulo para que clientes puedan realizar compras en lineas**

<br/>
    <h1>Codigo base de datos<h1/>

	
```SQL
Create database Inventario;
go
	
Use Inventario;
go
	
Create table Roles(
	RolId int identity primary key NOT NULL,
	Nombre varchar(50) NOT NULL
);
go

Create table Usuarios(
	UsuarioId int identity(1,1) primary key NOT NULL,
	Nombre varchar(50) NOT NULL,
	Contrasenia varchar(50) NOT NULL,
	RolId int default(2) NOT NULL,
	
	Foreign key (RolId) references Roles(RolId)
);
go

Create table Productos(
	ProductoId int identity(1,1) primary key NOT NULL,
	Nombre varchar(50) NOT NULL,
	Descripcion varchar(50) NOT NULL,
	Precio Decimal(16,2) NOT NULL,
	Cantidad int NOT NULL,
);
go

Create table Ventas(
	VentaId int identity(1,1) primary key NOT NULL,
	Titulo varchar(50) NOT NULL,
	Cliente varchar(50) NOT NULL,
	Fecha Date NOT NULL,
	ProductoId int NOT NULL,
	Cantidad int NOT NULL,
	Total Decimal(16,2) NOT NULL
	Foreign key (ProductoId) references Productos(ProductoId)
);
go

Create table Proveedores(
	ProveedorId int identity(1,1) primary key NOT NULL,
	Nombre varchar(50) NOT NULL,
	Telefono varchar(50) NOT NULL,
	Pais varchar(50) NOT NULL
);
go

Create table Compras(
	CompraId int identity(1,1) primary key NOT NULL,
	Titulo varchar(50) NOT NULL,
	Fecha Date NOT NULL,
	ProductoId int NOT NULL,
	Cantidad int NOT NULL, 
	ProveedorId int NOT NULL,
	Total Decimal(16,2) NOT NULL,
	Foreign key (ProductoId) references Productos(ProductoId),
	Foreign key (ProveedorId) references Proveedores(ProveedorId)
);
go
```



