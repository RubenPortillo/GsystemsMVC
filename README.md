# GsystemsMVC
Aplicación cliente MVC.

## Configuración
No se require configuración previa.

## Ejecución
Para poder ejecutar la aplicación solo será necesario descargar la solución y levantar el IISExpress desde Visual Studio.
La aplicación estará en el puerto 44374

También se puede levantar con Docker desde Visual studio.

## Uso

### Página login

Al iniciar la aplicación lo primero que deberemos hacer para poder consumir el servicio es logarnos con alguno de los siguientes usuarios o contraseñas:

- Datos login test:
  - Usuario 1:
    - mail: jgonzalezp@empresa1.com
    - password: 1234
  - Usuario 2:
    - mail: jgutierrezf@empresa1.com
    - password: 1234
  - Usuario 3:
    - mail: jsanromanc@empresa1.com
    - password: 1234
    
 Actualmente, los usuarios no tiene perfiles, todos "ven" todo.
 
  ![alt text](https://github.com/RubenPortillo/GsystemsMVC/blob/master/Properties/imgReadme/MVC1.jpg)
 
 
### Página lista usuario

Una vez logados, aparecerá la ventana con la lista de los usuarios.

  ![alt text](https://github.com/RubenPortillo/GsystemsMVC/blob/master/Properties/imgReadme/MVC2.jpg)
  
Es necesario hacer click en alguno de los nombres para poder acceder a los datos de las incidencias asigandas a cada uno.

### Página Incidencias

En esta página podremos ver el listado de las incidencias de cada empleado.

  ![alt text](https://github.com/RubenPortillo/GsystemsMVC/blob/master/Properties/imgReadme/MVC3.jpg)
  
También desde está página podremos crear una nueva incidencia para el empleado consultado. Para ello debemos hacer click en "Crear Nueva Incidencia" que mostrará el formulario de creación.

  ![alt text](https://github.com/RubenPortillo/GsystemsMVC/blob/master/Properties/imgReadme/MVC4.jpg)
  
En el registro, se puede asignar la prioridad de la incidencia, la ubicación y la descripción de la misma. Siendo campos automáticos el empleado al que se le asignará y la fecha de creación.

Al pulsar sobre "Crear" se actualizará la tabla con el nuevo registro.
Al pulsar sobre "Volver" en cualquiere momento, volveremos a la página de listado de empleados.


# Respuesta a cuestionario

## 1.1	MÉTODOS  QUE EXPONE LA API
Se explican en el Readme de la Api los métodos expuestos.

## 1.2	NOTIFICAR QUE SE HA CREADO UNA INCIDENCIA A UN EMPLEADO

Para este caso, tenemos varias opciones

 ### Servicio de Windows
 Se puede crear un servicio de windows que configurado cada pocos minutos para que consulte la tabla de INCIDENCIAS y si encuentra un nuevo registro, notifique al empleado en cuestión vía mail.
 
 ### SignalR
 Con esta librería se podrían lanzar notificaciones inmediatas al empleado mediantes notificaciones push en la plataforma.
 
 ## 1.3	BASE DE DATOS
 La query para SQLServer, sería la siguiente:
 ```sql
  SELECT TOP 1 EM.ID as EmpleadoID, Count(EM.ID) AS IncidenciasTotales 
  FROM Empresa E
  INNER JOIN Empleado EM ON  EM.EmpresaID = E.ID
  INNER JOIN Incidencia I ON I.EmpleadoID = EM.ID
  WHERE E.Nombre = 'Munity' and I.FIncidencia BETWEEN '2021-05-01' and '2021-05-31' 
  GROUP BY EM.ID  
  ORDER BY  IncidenciasTotales DESC
```

 
 ## 1.4 SEGURIDAD
 Se explica en el Readme de la Api que está securizado mediante JWT
