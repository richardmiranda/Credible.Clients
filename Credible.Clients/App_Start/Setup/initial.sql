USE [dev_test]
GO
SET IDENTITY_INSERT [dbo].[u_portal] ON 

GO
INSERT [dbo].[u_portal] ([portal_id], [portal_nm]) VALUES (1, N'ABC Company')
GO
INSERT [dbo].[u_portal] ([portal_id], [portal_nm]) VALUES (2, N'FCPS')
GO
INSERT [dbo].[u_portal] ([portal_id], [portal_nm]) VALUES (3, N'LCPS')
GO
INSERT [dbo].[u_portal] ([portal_id], [portal_nm]) VALUES (4, N'PWCPS')
GO
SET IDENTITY_INSERT [dbo].[u_portal] OFF
GO
SET IDENTITY_INSERT [dbo].[u_course_portal] ON 

GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (1, N'Management', 1)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (2, N'Management', 2)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (3, N'Management', 3)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (4, N'Management', 4)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (5, N'Ethics', 1)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (6, N'Ethics', 2)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (7, N'Economics', 3)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (8, N'Economics', 4)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (9, N'Statistics', 1)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (10, N'Statistics', 3)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (11, N'Physics', 2)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (12, N'Physics', 4)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (13, N'Humanity', 4)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (14, N'Humanity', 1)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (15, N'Sociology', 2)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (16, N'Sociology', 3)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (17, N'Algebra', 1)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (18, N'Algebra', 2)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (19, N'Algebra', 3)
GO
INSERT [dbo].[u_course_portal] ([course_portal_id], [course_portal_nm], [portal_id]) VALUES (20, N'Algebra', 4)
GO
SET IDENTITY_INSERT [dbo].[u_course_portal] OFF
GO
SET IDENTITY_INSERT [dbo].[u_registration] ON 

GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (1, 1, 1, CAST(N'2018-01-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (2, 1, 2, CAST(N'2018-01-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (3, 1, 3, CAST(N'2018-01-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (4, 1, 4, CAST(N'2018-01-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (6, 2, 1, CAST(N'2018-01-02 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (7, 3, 1, CAST(N'2018-01-02 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (8, 4, 1, CAST(N'2018-01-02 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (9, 5, 1, CAST(N'2018-01-02 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (11, 6, 1, CAST(N'2018-05-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (12, 7, 1, CAST(N'2018-05-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (13, 8, 1, CAST(N'2018-05-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (14, 9, 2, CAST(N'2018-05-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (15, 10, 2, CAST(N'2018-05-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (16, 11, 2, CAST(N'2018-05-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (17, 12, 3, CAST(N'2018-06-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (18, 13, 3, CAST(N'2018-06-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (19, 14, 3, CAST(N'2018-06-01 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (20, 15, 4, CAST(N'2018-06-15 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (21, 16, 4, CAST(N'2018-06-15 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (22, 17, 4, CAST(N'2018-06-15 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[u_registration] ([registration_id], [course_portal_id], [user_id], [registration_dttm]) VALUES (23, 18, 4, CAST(N'2018-06-15 00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[u_registration] OFF
GO
SET IDENTITY_INSERT [dbo].[u_user] ON 

GO
INSERT [dbo].[u_user] ([user_id], [first_nm], [last_nm]) VALUES (1, N'John', N'Doe')
GO
INSERT [dbo].[u_user] ([user_id], [first_nm], [last_nm]) VALUES (2, N'Jane', N'Doe')
GO
INSERT [dbo].[u_user] ([user_id], [first_nm], [last_nm]) VALUES (3, N'John', N'Smith')
GO
INSERT [dbo].[u_user] ([user_id], [first_nm], [last_nm]) VALUES (4, N'Jane', N'Smith')
GO
SET IDENTITY_INSERT [dbo].[u_user] OFF
GO
