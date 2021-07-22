# SAT Recruitment

El objetivo de esta prueba es refactorizar el código de este proyecto.
Se puede realizar cualquier cambio que considere necesario en el código y en los test.


## Requisitos 

- Todos los test deben pasar.
- El código debe seguir los principios de la programación orientada a objetos (SOLID, DRY, etc...).
- El código resultante debe ser mantenible y extensible.

## Refactorizaciones
 - Se eliminaron las clases Partials
 - Se inyecto un Singleton al controller para que tenga la lista de usuarios cargados
 - Se utilizo el patron DTO y se utiliza la Validation model
 - La funcion NormailizeEmail se creo dentro de una clase Static para Funciones Strings
 - La Salida de la Api se llevo a IResultObject
 - Se agregaron varios test para dar cobertura de codigo
 
 
 
 
 
 