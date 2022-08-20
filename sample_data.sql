
INSERT INTO public."function" (id,"module",code,"name",description,url,"path","method",parent_cd,"order",is_active,icon,created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('16576756-8265-4013-bf20-98fa153fbb39','Core','M0001','System setting','システム設定',NULL,NULL,NULL,NULL,3,true,'mdi-cogs','2022-07-28 21:43:51.941681','e5cd7986-51b5-4f19-bc4f-0756dfcf9089',NULL,NULL,false),
	 ('b63d0d18-9290-4cb3-adcd-7bb0b10b5af9','Core','M0002','Master','マスター管理',NULL,NULL,NULL,NULL,2,true,'mdi-cogs','2022-07-28 21:43:51.943593','38bdc9fc-c3b1-42e1-ba94-7daeec1a0294',NULL,NULL,false),
     ('626c6e6a-167e-4037-93f6-9063866a4af7','Core','S00020','Function','働き','/function','','','M0001',3,true,NULL,'2022-07-28 21:43:51.954385','b2dae0ad-a099-49f8-b804-398b05923554',NULL,NULL,false),
	 ('7000e0fc-53f1-4c35-b82e-f5500885c374','Core','S00030','Resource','資源','/resource',NULL,NULL,'M0001',4,true,NULL,'2022-07-28 21:43:51.911885','7b33c9ad-10b6-4ddf-ab57-1a9b653efce3',NULL,NULL,false),
	 ('813da262-1396-4771-9b78-ee48f4a70103','Core','S00010','Role','役割','/role',NULL,NULL,'M0001',2,true,NULL,'2022-07-28 21:43:51.952296','27d5b42a-a7de-4ec3-8ec0-a177ee01de6f',NULL,NULL,false),
	 ('8aa372a0-a186-4cca-9992-aff6a8076cf3','Core','S00040','Master code','マスターコード','/master',NULL,NULL,'M0001',5,true,NULL,'2022-07-28 21:43:51.950923','a2e24c68-2838-4d24-84ee-8544bd8af37f',NULL,NULL,false),
     ('5814f13f-254a-4fab-927b-62eb954e173b','Core','S00001','User','ユーザー','/user',NULL,NULL,'M0002',1,true,NULL,'2022-07-28 21:43:51.944275','4441a06d-ce3a-4e37-b375-fb3fa71eec56',NULL,NULL,false);

INSERT INTO public."function" (id,"module",code,"name",description,url,"path","method",parent_cd,"order",is_active,icon,created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('0b32ff5c-6c36-40fe-aa3a-d65c70000aea','Core','F00006','Delete user','ユーザーを削除',NULL,'api/user/{id}','DELETE','S00001',5,true,NULL,'2022-07-28 21:43:51.945564','e0201460-8694-4948-9c1f-d0b34552c251',NULL,NULL,false),
	 ('1ec680f6-5929-46dd-85f1-8fb9685d9955','Core','F00001','Change user password','ユーザーパスワードの変更',NULL,'api/auth/change-password','PUT',NULL,NULL,true,NULL,'2022-07-28 21:43:51.93809','33f2b725-a42e-4d9f-91a7-7ef2af838f7c',NULL,NULL,false),
	 ('2b82e366-ccae-4f1f-9b75-17aa3533e80d','Core','F00015','Delete multi role','マルチロールを削除',NULL,'api/role/delete-multi','DELETE','S00010',6,true,NULL,'2022-07-28 21:43:51.950208','414670bc-d0fd-419a-892d-ab7cf1790b59',NULL,NULL,false),
	 ('2dc1af07-3b05-45c6-b68a-66b03c802ed1','Core','F00011','Get role by id','IDで役割を取得',NULL,'api/role/{id}','GET','S00010',2,true,NULL,'2022-07-28 21:43:51.947557','8cf95544-85ce-4d36-b985-13a7232dff0f',NULL,NULL,false),
	 ('3ac50392-55e0-48e3-9fcf-56f5344ec95a','Core','F00000','User login info','ユーザーログイン情報','','api/auth/info','GET',NULL,1,false,NULL,'2022-07-28 21:43:51.952982','3367d05d-e837-46ef-b7d7-4b439b2f2813',NULL,NULL,false),
	 ('3c7f8c33-d533-4043-8fdc-784ba3bebaf6','Core','F00016','Get list role','リストの役割を取得する',NULL,'api/role','GET','S00010',1,true,NULL,'2022-07-28 21:43:51.958818','eeb9079e-c33c-44a0-a6ae-59beee75cb58',NULL,NULL,false),
	 ('42d28900-2307-4936-bf03-4c2e9c861bd9','Core','F00002','Get list user','リストユーザーを取得',NULL,'api/user','GET','S00001',1,false,NULL,'2022-07-28 21:43:51.939714','0a1f2551-8a60-45f3-bba8-98bf836c8880',NULL,NULL,false),
	 ('4e04235e-4f06-4182-aade-642989aa6861','Core','F00005','Update user','ユーザーを更新',NULL,'api/user/{id}','PUT','S00001',4,true,NULL,'2022-07-28 21:43:51.94496','d64039e6-8a0c-4fd5-a47a-4146a2235ac6',NULL,NULL,false),

	 ('6040735a-1158-44a2-8cba-94d0d38edf34','Core','F00013','Update role','役割を更新',NULL,'api/role/{id}','PUT','S00010',4,true,NULL,'2022-07-28 21:43:51.948849','ecde3b60-70b7-4af4-add2-f6f05adfa68d',NULL,NULL,false);
INSERT INTO public."function" (id,"module",code,"name",description,url,"path","method",parent_cd,"order",is_active,icon,created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('adb25282-39db-4eb9-80e2-c575a499131c','Core','F00012','Create new role','新しい役割を作成する',NULL,'api/role','POST','S00010',3,true,NULL,'2022-07-28 21:43:51.948156','ccecd4d2-d110-4d3d-a324-1d424b3ad94d',NULL,NULL,false),
	 ('afc3a308-a2c9-4c35-8488-1225b11d7504','Core','F00007','Delete multi user','マルチユーザーを削除する',NULL,'api/user/delete-multi','DELETE','S00001',6,true,NULL,'2022-07-28 21:43:51.946864','ff6ad19d-afad-479d-8399-7158be8093b8',NULL,NULL,false),
	 ('c598a7fd-344b-4b88-9d25-e63e4339319e','Core','F00003','View detail user','詳細ユーザーを表示',NULL,'api/user/{id}','GET','S00001',2,true,NULL,'2022-07-28 21:43:51.946254','b6989fc4-0fc0-4bbe-aac9-a84573ed6e8a',NULL,NULL,false),
	 ('ce872625-43fa-46b1-8cb9-9469a911d053','Core','F00014','Delete role','役割を削除する',NULL,'api/role/{id}','DELETE','S00010',5,true,NULL,'2022-07-28 21:43:51.949494','7ef4003b-b212-441e-ac15-8fcdb4fb93fa',NULL,NULL,false),
	 ('dcb39b81-3453-44b1-8ba4-7e6281cc27aa','Core','F00004','Create user','ユーザーを作成',NULL,'api/user','POST','S00001',3,true,NULL,'2022-07-28 21:43:51.939072','bc9731f7-e37a-4c1a-a76b-57bfc5873743',NULL,NULL,false),
	 ('2c0035d2-d44d-4dc1-bb79-352c6d526259','Core','S00000','Dashboard','ダッシュボード','/','',NULL,NULL,1,true,'mdi-view-dashboard','2022-07-28 21:43:51.951611','2807fba6-bd62-485d-8b2f-17506e1d88ae',NULL,NULL,false);


INSERT INTO public.master_code (id,"type","key",value,created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('365b5ac3-119a-4eab-9928-415b4060bd10','GENDER','2','Other','2022-06-24 07:23:39.15534','7b2dfc24-841c-422f-b3c6-b3846f93be5d',NULL,NULL,false),
	 ('9f114a2d-6f24-4289-8eb8-d3f568816fd6','GENDER','0','Male','2022-06-24 06:11:11.863111','7b2dfc24-841c-422f-b3c6-b3846f93be5d',NULL,NULL,false),
	 ('c419017a-eaa3-4c99-bcfa-c60eb37e2291','MODULE','Core','Core','2022-06-24 06:11:40.546665','7b2dfc24-841c-422f-b3c6-b3846f93be5d',NULL,NULL,false),
	 ('c419017a-eaa3-4c99-bcfa-c60eb37e2292','GENDER','1','Female','2022-06-24 06:11:40.546665','7b2dfc24-841c-422f-b3c6-b3846f93be5d',NULL,NULL,false),
	 ('c419017a-eaa3-4c99-bcfa-c60eb37e2293','MODULE','Master','Masterv','2022-06-24 06:11:40.546665','7b2dfc24-841c-422f-b3c6-b3846f93be5d',NULL,NULL,false);


INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('26277f05-9841-4c2b-8cd8-818c2393533b','ja','Core','User','Phone','Phone','2022-07-28 21:43:52.054744','15917844-b2a0-4839-92b6-c92f3a697136',NULL,NULL,false),
	 ('0c9b73fa-b1cc-47c4-a7d9-4f8f9af0db16','ja','Core','User','Role','Role','2022-07-28 21:43:52.055512','7121dc74-6254-4073-82bd-583b4329798a',NULL,NULL,false),
	 ('0c06a684-0225-4031-aa85-f6dbbabcb8a9','ja','Core','Common','NumNoTitle','#','2022-07-28 21:43:51.980111','92b5a963-f4fc-4254-8c5b-e0749f301aa4',NULL,NULL,false),
	 ('21b1b5a3-9547-4c64-8c9d-1c479b87fc88','ja','Core','Common','Action','Action','2022-07-28 21:43:51.966788','bd514db2-ca0a-4577-9146-c6eb8efff093',NULL,NULL,false),
	 ('18732ab5-cfac-4d4d-a804-8d71ad6070cc','ja','Core','Common','BtnAddNew','Add','2022-07-28 21:43:51.967366','8054c77b-450f-4d58-a6e7-31082ccd295e',NULL,NULL,false),
	 ('0ea298b8-e6b6-4d0f-b6ef-df7962ebc4d3','ja','Core','Common','ValidateInvalid','無効な{0}。','2022-07-28 21:43:52.000666','41082571-11db-4d73-8285-a4ca714f0060',NULL,NULL,false),
	 ('109c4624-1757-415b-84d6-de63e020e406','ja','Core','Message','I_001','データ登録が完了しました。','2022-07-28 21:43:52.038378','4be1c01a-1eca-4b38-914d-bd8e6816f96f',NULL,NULL,false),
	 ('50234513-7af6-44ba-8088-e2d401f00df4','ja','Core','Common','BtnExportData','Export','2022-07-28 21:43:51.979274','eb7eafdf-2677-4603-9d92-a188adf96eee',NULL,NULL,false),
	 ('0df342f0-b202-4998-80c9-0b3fa153c130','ja','Core','User','UserName','User name','2022-07-28 21:43:52.057041','0b3edb0a-3e0c-4850-8780-4b81d2ee16b8',NULL,NULL,false),
	 ('14dcad9e-6108-41a4-9d00-0c64ed4fe570','ja','Core','Common','ValidateMaxLength','{0}は{1}より小さくする必要があります。','2022-07-28 21:43:52.003721','cbc214aa-773d-4473-b1e4-ff748128ce57',NULL,NULL,false);
INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('16c7e526-6445-405f-a03a-10af9bd7c4af','ja','Core','Common','ValidateMinLength','{0}は{1}より大きくなければなりません。','2022-07-28 21:43:52.004587','c16f5e9d-1a35-4321-8ad8-fd887c3384cd',NULL,NULL,false),
	 ('1b9d0873-2f86-4f6e-b9d5-ba3c6150d9e3','ja','Core','Common','ValidateDateSameOrBefore','{0}は{1}を以降しないとなりません。','2022-07-28 21:43:52.007058','cfcf81ff-a3e6-464b-b7ad-e8d9fd1406f0',NULL,NULL,false),
	 ('a4b50cbf-1ec8-4e18-bcb0-01e1221a2458','ja','Core','Role','DeleteSuccess','Delete success','-infinity','00000000-0000-0000-0000-000000000000',NULL,NULL,false),
	 ('4a9791fa-0033-458e-8786-f06755284404','ja','Core','Function','Api','Api','2022-07-28 21:43:52.009341','530f8656-929f-4bec-b110-f9936eb2b683',NULL,NULL,false),
	 ('0df342f0-b202-4998-80c9-0b3fa153c131','ja','Core','Role','Code','Code','2022-07-28 21:43:52.057041','0b3edb0a-3e0c-4850-8780-4b81d2ee16b8',NULL,NULL,false),
	 ('2875cfe9-e49f-4c81-8d93-884f5cd84c1f','ja','Core','Function','Description','Description','2022-07-28 21:43:52.014053','dc295063-f4c4-4c40-8665-03307581b238',NULL,NULL,false),
	 ('1fe70e29-645a-479a-ac3a-f9f9574406ab','ja','Core','Common','BtnExportExcel','Export excel','2022-07-28 21:43:51.972982','b44fd0d7-b65e-4b1a-bae7-c0821a4c8011',NULL,NULL,false),
	 ('0ee66e66-39e6-4b9c-bee3-c7f4aa7b7bdb','ja','Core','Common','BtnSave','Save','2022-07-28 21:43:51.976294','278d99b8-6405-4139-a31c-a59aa6ebe531',NULL,NULL,false),
	 ('0df342f0-b202-4998-80c9-0b3fa153c133','ja','Core','Role','Description','Description','2022-07-28 21:43:52.057041','0b3edb0a-3e0c-4850-8780-4b81d2ee16b8',NULL,NULL,false),
	 ('0df342f0-b202-4998-80c9-0b3fa153c132','ja','Core','Role','Name','Name','2022-07-28 21:43:52.057041','0b3edb0a-3e0c-4850-8780-4b81d2ee16b8',NULL,NULL,false);
INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('13bd5c18-60ac-4f9b-98b5-f99a2bf6b4f6','ja','Core','Common','BtnSearch','Search','2022-07-28 21:43:51.977704','b8a30216-34b8-423f-935b-5f6427b072cd',NULL,NULL,false),
	 ('0a6485e4-de2a-408a-9912-39baa8ac2ec7','ja','Core','Common','Logout','Logout','2022-07-28 21:43:51.989577','2b984506-cf1f-4ae4-a1b7-b6cb7d7559b1',NULL,NULL,false),
	 ('4eda2f3b-9472-429b-929f-e68900224f66','ja','Core','Common','BtnOk','Ok','2022-07-28 21:43:51.974128','7d165323-fa42-48b2-915d-54ec469005f7',NULL,NULL,false),
	 ('3872ba29-5e90-45b0-b820-3ac696733286','ja','Core','Common','LogoutConfirm','Are you sure?','2022-07-28 21:43:51.990489','ac065958-777a-4c80-93c6-f1ff193a87dd',NULL,NULL,false),
	 ('35da3c8e-992d-4672-8e14-71367706f505','ja','Log','Common','MessageLog','Log','2022-07-28 21:43:52.099834','d67a6174-6c96-46ba-93ad-2286a0200403',NULL,NULL,false),
	 ('55912b37-2b8d-4e01-85d6-2db9905bc54a','ja','Core','Common','ValidateFieldRequied','{0}が必要です。','2022-07-28 21:43:51.999017','400cffe9-ae47-4695-9417-bff54a78ccda',NULL,NULL,false),
	 ('0529d9ac-cdc4-48e3-8c8f-4c838067edcb','ja','Core','Function','Code','Code','2022-07-28 21:43:52.01113','5801b100-c82d-4b63-aefd-02a59a98f55b',NULL,NULL,false),
	 ('2a32c349-8870-479f-9f93-2a2a6c033b08','ja','Core','Function','Parent','Parent','2022-07-28 21:43:52.019478','9ae5ac91-6d13-4263-a8fd-55d4a542f958',NULL,NULL,false),
	 ('49e2be10-1bf1-4b38-beac-310d8e4cee71','ja','Core','Login','UserName','Username','2022-07-28 21:43:52.025256','373d3ecb-3366-4c43-88b3-23548a82c8d1',NULL,NULL,false),
	 ('3785c99a-64f4-421a-81a0-d45c12b6c20f','ja','Core','MasterCode','Value','Value','2022-07-28 21:43:52.030036','664eeb3c-a9d9-4c9c-a951-36469f0a44f9',NULL,NULL,false);
INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('13bbadc7-f2cb-446b-a5d2-051afdcc57fa','ja','Core','Message','Success','Success','2022-07-28 21:43:52.114189','4c04271e-b674-4e0d-8cf8-3648c133902b',NULL,NULL,false),
	 ('137c63d4-84ea-4d9f-8eb5-9d80d55e5637','ja','Core','Resource','Module','Module','2022-07-28 21:43:52.042664','8ebfd2e9-425a-45ec-b183-6e244c3468ab',NULL,NULL,false),
	 ('15756bf5-cc71-46ad-8b44-8bf2e0615104','ja','Core','Resource','Screen','Screen','2022-07-28 21:43:52.043442','95c7dc16-6a4f-49a6-9175-bdb8ce316536',NULL,NULL,false),
	 ('6145f521-60c7-4464-9bfe-ae6d754993ca','ja','Core','Role','Permission','Permission','2022-07-28 21:43:52.057','0b3edb0a-3e0c-4850-8780-4b81d2ee16b8',NULL,NULL,false),
	 ('529991e2-c9fd-49d9-b141-a22b5ecb89f4','ja','Core','User','Email','Email','2022-07-28 21:43:52.049938','dbd6c3cd-4b39-417a-b382-f2d078054ecf',NULL,NULL,false),
	 ('02c66e85-d88c-4c9a-8318-b8d756839df6','ja','Core','User','OldPassWord','Old password','2022-07-28 21:43:52.053344','65db721f-bc13-42b5-9452-9d96d1976673',NULL,NULL,false),
	 ('84a30138-6701-492a-9c5a-bc10f930dbe3','ja','Core','MasterCode','Key','Key','2022-07-28 21:43:52.027968','744dc653-7a7f-4ce8-9a9a-c75ebea8282e',NULL,NULL,false),
	 ('90674c26-fbd8-49e7-8f62-da4f9ffa1542','ja','Core','Resource','Key','Key','2022-07-28 21:43:52.041968','bc5e6c62-330b-41d3-b73d-545e654da716',NULL,NULL,false),
	 ('ab2df1c7-8182-4815-acaf-43dc644aa61b','ja','Core','Role','UpdateSuccess','Update success','-infinity','00000000-0000-0000-0000-000000000000',NULL,NULL,false),
	 ('7326db6d-d4f0-45c7-a772-3a247c1fd2e5','ja','Core','User','Active','Active','2022-07-28 21:43:52.007891','6248fc98-c861-4bbe-bfea-b36d4534b7bd',NULL,NULL,false);
INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('637d73b2-4e14-4a25-aa0a-04c625a1cc8d','ja','Core','Function','Api','Api','2022-07-28 21:43:52.010216','e4a05e67-4d60-458d-86f3-6d0b46a96434',NULL,NULL,false),
	 ('98afe522-4b65-4b99-b519-b399e99c5a47','ja','Core','User','Code','Code','2022-07-28 21:43:52.046554','1db7e51c-ebee-4555-a28b-79c27a66fd39',NULL,NULL,false),
	 ('5b50da35-4f70-4a8c-800b-63dd254ba33e','ja','Core','User','Gender','Gender','2022-07-28 21:43:52.052004','96aa17b9-8998-4932-89c7-42f9c428fde2',NULL,NULL,false),
	 ('5d2f482b-1abf-405d-a396-48a710fd0973','ja','Core','Function','Order','Order','2022-07-28 21:43:52.018717','9e928548-2730-447b-8d90-1c99276b5666',NULL,NULL,false),
	 ('585f148f-ccfd-482e-b1f7-a5c240aeb993','ja','Core','Message','W_002','W_002','2022-07-28 21:43:52.113497','95cf6d18-db75-4a58-869c-31f3c88cd129',NULL,NULL,false),
	 ('c443fb72-0d4c-4845-8351-9eceed1f7b7b','ja','Core','Message','I_005','I_005','2022-07-28 10:05:36.519431','668a8b16-7bd3-4a7a-88d0-38a2c16a9532',NULL,NULL,false),
	 ('105db188-117a-408f-8804-3b5f07d3a857','ja','Core','Common','BE_004','要求されたリソースにアクセスする権限がありません','2022-07-28 21:43:51.993044','6ce05a04-c3ff-4e03-99ba-83acf8ce18ce',NULL,NULL,false),
	 ('876a2706-3518-41f5-86cf-0031221be414','ja','Core','Cache','ClearAllSuccess','Clear all success.','2022-07-28 21:43:51.966153','48319244-381d-4faf-9926-f37c13b7a534',NULL,NULL,false),
	 ('6af99014-e34a-4c92-b3bf-8f064af2d54a','ja','Core','Common','BtnClear','Clear','2022-07-28 21:43:51.970014','8e5d859c-bce2-44bf-a515-6ec0b959c2f8',NULL,NULL,false),
	 ('6969329c-48a8-416e-a61b-fd9293123daa','ja','Core','Common','BtnConfirm','Confirm','2022-07-28 21:43:51.970663','54ae093e-14ba-4289-a165-0116870b8b03',NULL,NULL,false);
INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('70fa2992-78df-43b7-8da5-7293f8eb3413','ja','Core','Common','BtnDeleteMulti','Delete','2022-07-28 21:43:51.972204','c24a2fc9-649a-4578-9ff0-743d8e7186fc',NULL,NULL,false),
	 ('695e4675-c799-42f4-a444-b781cc34723e','ja','Core','Common','BtnRegister','Register','2022-07-28 21:43:51.975626','ec1d7fe7-fa66-49b4-807e-e097c5c72e29',NULL,NULL,false),
	 ('93bb419b-acce-404f-a452-0adb22aad50b','ja','Core','Common','Profile','Profile','2022-07-28 21:43:51.994825','3712e66e-0dcf-4b90-b93e-59a72040b73b',NULL,NULL,false),
	 ('806ac442-cb79-4745-a650-4b3c1aa358bf','ja','Core','Common','RequestContentInvalid','Request content invvalid','2022-07-28 21:43:51.995749','8aa1eafb-522d-46ea-9c88-b7208fe5da9b',NULL,NULL,false),
	 ('71784aaa-9064-400c-96af-2fcf6e8ad0c2','ja','Core','Common','Settings','Settings','2022-07-28 21:43:51.996636','854c94a2-e856-4a32-9608-bc3d59f8890e',NULL,NULL,false),
	 ('969e1883-fd12-44a1-b7b0-09a3e7ef1195','ja','Core','Function','Method','Method','2022-07-28 21:43:52.014993','d9ad3400-239f-4a55-8d28-1eb7541541cc',NULL,NULL,false),
	 ('72b03df3-a8ed-445b-9e98-cf6f45764877','ja','Core','Message','E_004','データのエクスポートに失敗しました。','2022-07-28 21:43:52.034266','b6f73173-48b9-4893-8914-dd2fea95f4a7',NULL,NULL,false),
	 ('72d7f049-b76b-48ee-986c-2a1d96da7505','ja','Core','Message','E_005','データが存在しません','2022-07-28 21:43:52.03487','d6dded1d-6aa4-4450-bb52-849f421e6a97',NULL,NULL,false),
	 ('77d63b23-9e8f-40a9-b1cc-b9202ab71b5e','ja','Core','Message','E_009','データが必要です。','2022-07-28 21:43:52.036858','8bfd6839-bafe-4d74-817d-f92939b431d1',NULL,NULL,false),
	 ('6373b54f-e207-4649-8ea3-ed14eec58b80','ja','Core','Function','Module','Module','2022-07-28 21:43:52.016608','0300dd8a-add4-4218-aa6c-5c4422e372e7',NULL,NULL,false);
INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('92e3e7d1-43b7-4bed-8a53-93ba6fb540cf','ja','Core','Login','Password','Password','2022-07-28 21:43:52.024566','042798b8-8437-4300-bf4e-b779548acd4a',NULL,NULL,false),
	 ('9312e923-6aca-4255-b46d-3ac6992ee019','ja','Core','Message','I_003','データの削除が完了しました。','2022-07-28 21:43:52.039896','8bcf4805-a6df-4379-a9a5-cf5d332fc2f2',NULL,NULL,false),
	 ('98d8658c-cb19-43f9-bb0d-bd99e971e8bb','ja','Core','Message','E_008','リクエストコンテンツが無効です','2022-07-28 21:43:52.036155','292043af-e4ed-4407-822c-587809bf7f10',NULL,NULL,false),
	 ('9aa6d701-3fed-42fe-9f92-9b92cc31b85f','ja','Core','Message','E_010','E_010','2022-07-28 21:43:52.037618','cb5b9e16-989e-4ea4-9ebe-ecc05698aadc',NULL,NULL,false),
	 ('9d1c3c92-67a2-40da-abed-5722fe4b1583','ja','Core','Common','ConfirmDelete','削除<b>{0}</b>を確認しますか？','2022-07-28 21:43:51.981408','789a235d-b131-4cc5-b242-d679c7736d8f',NULL,NULL,false),
	 ('8d000f2d-9a4a-4589-b097-1130b9953234','ja','Core','Message','BE_003','BE_003','-infinity','00000000-0000-0000-0000-000000000000',NULL,NULL,false),
	 ('afef4004-a039-4631-b856-6d5b3b03a6dc','ja','Core','Common','BtnCancel','Cancel','2022-07-28 21:43:51.969131','081470b1-cf95-4e95-a12f-23740f74d8c9',NULL,NULL,false),
	 ('d6945245-19aa-4fe3-8024-c2e94b93185c','ja','Core','Common','Delete','Delete','2022-07-28 21:43:51.984793','c47efda3-f52f-4094-bf77-c8616d52863f',NULL,NULL,false),
	 ('c51e807e-94d1-42bd-9a62-dabe77cefee7','ja','Core','Common','HasError','Has error','2022-07-28 21:43:51.98881','4a5bf46e-5147-4c86-9b79-cf562e2ea3bb',NULL,NULL,false),
	 ('b2ad441f-c7a3-4aa0-9418-42c2d457f45d','ja','Core','Common','UpdateSuccess','Update success','2022-07-28 21:43:51.997448','aace4800-20b4-4252-8a9d-0e28cfbae19b',NULL,NULL,false);
INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('b4bf7abb-5057-4f05-b510-d9e74e83e0f4','ja','Core','MasterCode','Type','Type','2022-07-28 21:43:52.028659','cffea462-2e1d-43f6-83df-96eeeaf7d7c8',NULL,NULL,false),
	 ('d2818272-3f8c-4921-b23c-5475243474fc','ja','Core','User','NewPassWord','New password','2022-07-28 21:43:52.052678','64cda170-25b5-4abc-9c6a-e5a45e3407da',NULL,NULL,false),
	 ('ac585cbd-132d-4479-833e-0050b381f2ec','ja','Core','User','OtherPassWord','Confirm password','2022-07-28 21:43:52.054032','3a939ef8-b18b-48c0-a93f-67198994dbab',NULL,NULL,false),
	 ('585f148f-ccfd-482e-b1f7-a5c240aeb997','ja','Core','Message','W_012','W_012','2022-07-28 21:43:52.113497','95cf6d18-db75-4a58-869c-31f3c88cd129',NULL,NULL,false),
	 ('a7346207-ddce-43b1-b767-52fdb651f498','ja','Core','Common','ValidateLength','{0}は{1}文字を超えてはなりません。','2022-07-28 21:43:52.002362','1b5e8726-8657-42d5-b230-925ffaf15a3b',NULL,NULL,false),
	 ('affe9da1-3cad-4e66-b79a-a9da2baddf44','ja','Core','Message','I_002','データの更新が完了しました。','2022-07-28 21:43:52.039138','233bf011-b9ee-4f21-b9a4-54b1bd2e4970',NULL,NULL,false),
	 ('b6cd7cae-f820-418c-91b1-70719cd1d2b4','ja','Core','Common','ConfirmDeleteMulti','<b>{0}</b> レコードの削除を確認しますか？','2022-07-28 21:43:51.983298','7b9dd1e5-e19d-4305-9443-74a6f66b1503',NULL,NULL,false),
	 ('d4cd330c-0777-44ae-a97c-cc655722a99d','ja','Core','Common','PagingText','<b>{2}</b>エントリの<b>{0}</b>から<b>{1}</b>を表示','2022-07-28 21:43:51.993904','fe36ab44-2197-42a3-baa0-8a46d787d4ed',NULL,NULL,false),
	 ('dad35d6d-1b59-4f8e-9433-442ea5384a26','ja','Core','Message','E_007','データが見つかりません','2022-07-28 21:43:52.035518','4f4c2ec7-add3-4ff0-aae9-d26387246d42',NULL,NULL,false),
	 ('df5ff503-a37a-4a02-864a-6d2cdd1caa01','ja','Core','Message','E_001','データ登録が失敗した。','2022-07-28 21:43:52.031644','e304892a-2fbd-42db-9552-45089df6cb21',NULL,NULL,false);
INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('7d0066e4-9b0c-4586-8954-6070d928c3e8','ja','Core','MasterCode','DeleteMultipleSuccess','DeleteMultipleSuccess','-infinity','00000000-0000-0000-0000-000000000000',NULL,NULL,false),
	 ('e3d65c2f-bd62-476f-9df6-28c1c26e8dde','ja','Core','Common','ValidateDateBefore','{0}は{1}をを明日からしないとなりません。','2022-07-28 21:43:52.005932','6bff2717-7999-4b5d-b3c7-793a19219d0c',NULL,NULL,false),
	 ('e62ed820-d41b-4826-bee4-16327d58ac7e','ja','Core','Message','E_002','データの更新に失敗しました。','2022-07-28 21:43:52.032696','630321b7-7f61-40c1-bfec-5cd6de6e65f7',NULL,NULL,false),
	 ('ff65c0aa-67a5-4d9c-9454-9a82d90d1e29','ja','Core','Function','Url','Url','2022-07-28 21:43:52.023218','f07680d6-d3d1-4b05-817a-9c132f0b7aab',NULL,NULL,false),
	 ('e893bf18-794f-4e43-969b-fcb94d08e52e','ja','Core','Message','E_003','データは削除できません。','2022-07-28 21:43:52.033618','a4c2cafb-418a-458b-80c8-73bfeb6d3d31',NULL,NULL,false),
	 ('e453d92a-170a-46f1-a811-e7abc18ab978','ja','Core','Common','BtnBack','Back','2022-07-28 21:43:51.968044','e2f6eb03-5854-4370-ac8f-d5c914f8070b',NULL,NULL,false),
	 ('eb882cdd-c063-4816-ac67-223ef9374cea','ja','Core','Common','BtnDelete','Delete','2022-07-28 21:43:51.971345','8e1877f8-820b-451d-90db-3989638cd035',NULL,NULL,false),
	 ('f35f8069-ed62-47c6-9841-13390b44f739','ja','Core','Common','NoData','No data !','2022-07-28 21:43:51.991348','fb849471-2e72-416c-bfdb-152386939f27',NULL,NULL,false),
	 ('f714d373-cfe8-4f9b-93d0-b1923deb9ca9','ja','Core','Common','NoRecordSelected','No item selected.','2022-07-28 21:43:51.992213','5f30c9f2-9d5f-4471-91dc-d61b9502162d',NULL,NULL,false),
	 ('ec791fac-d66f-4ba0-a998-9abf535bc154','ja','Core','Common','UpdateError','Update error','2022-07-28 21:43:52.11037','1970f4fd-d4cd-4349-a09e-eec152b55ece',NULL,NULL,false);
INSERT INTO public.resource (id,lang,"module",screen,"key","text",created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('f050f8fd-be6f-46a1-b0dc-7163f4c05ce1','ja','Core','Login','BtnLogin','Logign','2022-07-28 21:43:52.023842','e842ea01-d6e0-4283-bb18-96a054620261',NULL,NULL,false),
	 ('f4049f89-87ea-4e9e-b55b-1d720caabb94','ja','Core','Resource','Value','Value','2022-07-28 21:43:52.044991','841add78-003e-42f8-84a3-ecc4dad6f3d4',NULL,NULL,false),
	 ('e47fa997-3ab0-4b4f-ab1a-0dbe277dc1ed','ja','Core','User','FullName','Full name','2022-07-28 21:43:52.051266','1cfb9f08-09a6-4fa0-8fa6-1ec008bad47b',NULL,NULL,false),
	 ('ffcca51a-0f51-4e73-962a-fe9787eb5284','ja','Core','Function','Name','Name','2022-07-28 21:43:52.017876','3023eb0a-29eb-4529-a8d6-a7e94dc5565e',NULL,NULL,false);


INSERT INTO public."role" (id,code,"name",description,is_actived,created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('cd8986bb-7ee4-4abf-8b00-b4491653a117','ACCOUNT','Role for accounting','Role for accounting','1','2022-06-24 02:19:55.596107','7b2dfc24-841c-422f-b3c6-b3846f93be5d','2022-07-21 15:43:50.406644','668a8b16-7bd3-4a7a-88d0-38a2c16a9532',false),
	 ('0ecb5b7d-2f6a-44e2-aa26-4e1403097488','ADMIN','Administrator','Administrator','1','2022-02-02 00:00:00','bf4cc221-b403-4052-9dc7-deb75854a71d','2022-07-21 15:46:15.838332','668a8b16-7bd3-4a7a-88d0-38a2c16a9532',false);


INSERT INTO public."user" (id,code,full_name,user_name,hashpass,salt,mail,phone,is_actived,created_at,created_by,updated_at,updated_by,del_flg) VALUES
	 ('668a8b16-7bd3-4a7a-88d0-38a2c16a9532','U0000002','Lưu Công Thìn','thinlc','IALoWCDj0OYgJonk7YDw2YDOFXBDBOlGhsUclbeBPn4=','s1h6Agk7JuTBd1LmjcIR0RvW9GE=','thinlc@gmail.com','+840987654321',true,'2022-06-25 07:47:34.210469','7b2dfc24-841c-422f-b3c6-b3846f93be5d','2022-07-21 15:25:16.088436','668a8b16-7bd3-4a7a-88d0-38a2c16a9532',false),
	 ('a2bada29-a252-4728-98ed-0b25e92e1a9e','U0000003','Administrator','admin','cvBft9AdxRHCpnuWjdiHdJDSlwZCmVGl83Lh1Z7dbiI=','ihEyraM3PtVLoU3YWENjk+R43R8=','admin@gmail.com','+840987654322',true,'2022-07-21 15:06:44.610282','668a8b16-7bd3-4a7a-88d0-38a2c16a9532','2022-07-21 15:25:16.088436','668a8b16-7bd3-4a7a-88d0-38a2c16a9532',false);
