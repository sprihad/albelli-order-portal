
Steps to run the project :

1. git clone https://github.com/sprihad/AlbelliOrderPortal.git
2. Create Database named as AlbelliDb.
3. Run below commands in Package Manager console
	(i) add-migration <migration_name>
	(ii) update-database
4. The above commands will create necessary tables named 'Order' and 'OrderLine'
5. Run the Api and go to swagger ui (https://localhost:44316/swagger/index.html) to place and retrieve orders.
