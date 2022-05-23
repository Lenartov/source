select * from Blocks;
set identity_insert Blocks on;
insert into Blocks (Id, Data_Content, Data_Hash, Created, Hash, PrevHash, User_Login, User_Role, User_Password, User_Hash, BlockType) values(500, 'asdsadsa','www', 2022-05-23, 'asd', 'asdsad', 'asdasd', 2, 'qwewqe', 'qweqweqw', 1);
select * from Blocks;