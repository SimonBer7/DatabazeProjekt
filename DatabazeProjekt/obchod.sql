
create table adresa(
	id int primary key identity(1,1),
	ulice varchar(30) not null,
	psc int not null,
	mesto varchar(30) not null
);


create table zakaznik(
	id int primary key identity(1,1),
	jmeno varchar(20) not null,
	prijmeni varchar(20) not null,
	email varchar(50) not null,
	adresa_id int foreign key references adresa(id)
);


create table typ(
	id int primary key identity(1,1),
	nazev varchar(30) not null
);

create table produkt(
	id int primary key identity(1,1),
	nazev varchar(30) not null,
	cena_ks float not null check(cena_ks > 0),
	typ int not null foreign key references typ(id)
);

create table objednavka(
	id int primary key identity(1,1),
	cislo_obj int not null check(cislo_obj > 0),
	datum date not null default(format (getdate(), 'yyyy-mm-dd')),
	zakaznik_id int foreign key references zakaznik(id),
	produkt_id int foreign key references produkt(id),
	cena float not null check(cena > 0),
	zaplaceno bit not null
);

drop table objednavka;
select * from objednavka;
