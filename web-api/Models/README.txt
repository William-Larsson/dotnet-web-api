These are classes meant for to be used with the database,
but should not be used for the CRUD-operations, because they contain
unnecessary field, such as Id, which the user should not have to deal with. 

CRUD operations should be managed by the respective ViewModel class, 
and then converted into these Model classes before DB operations are made. 