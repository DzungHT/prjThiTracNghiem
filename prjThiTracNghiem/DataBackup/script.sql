USE [dbThiTracNghiem]
GO
/****** Object:  StoredProcedure [dbo].[sp_Baithi_Insert]    Script Date: 12/15/2016 6:58:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Baithi_Insert]
@DethiID int,
@SinhvienID int,
@Ngaylambai nvarchar(50),
@id int out
as
begin
	insert into BaiThi (DethiID,SinhVienID,NgayLamBai) values (@DethiID,@SinhvienID,@Ngaylambai)
	set @id=(select SCOPE_IDENTITY())
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Cauhoibaithi_Insert]    Script Date: 12/15/2016 6:58:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Cauhoibaithi_Insert]
@BaithiID int,
@CauhoiID int,
@Thutu int
as
begin
	INSERT INTO [dbo].[CauHoiBaiThi]
           ([BaiThiID]
           ,[CauHoiID]
           ,[ThuTu])
     VALUES
           (@BaithiID,@CauhoiID,@Thutu)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_LayDanhSachDeThi]    Script Date: 12/15/2016 6:58:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_LayDanhSachDeThi]
as
begin
	select 
	det.DeThiID as[ID],
	det.MaDeThi as [Mã đề thi],
	det.TenDeThi as [Tên đề thi],
	det.ThoiGian as [Thời gian],
	hp.TenHocPhan as [Học phần],
	dt.TenDotThi as [Đợt thi],
	isnull(ch.SoCauHoi,0) as [Tổng số câu hỏi]	
	from DeThi det
		inner join HocPhan hp on hp.HocPhanID=det.HocPhanID
		inner join DotThi dt on dt.DotThiID=det.DotThiID
		left join (Select  chdt.DeThiID, Count(chdt.DeThiID) as SoCauHoi FROM CauHoiDeThi chdt GROUP BY chdt.DeThiID) as ch on ch.DeThiID = det.DeThiID
	where det.HienThi=1;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Traloibaithi_Insert]    Script Date: 12/15/2016 6:58:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_Traloibaithi_Insert]
@BTid int,
@DAid int
as
begin
INSERT INTO [dbo].[TraLoiBaiThi]
           ([BaiThiID]
           ,[DapAnCauHoiID])
     VALUES
           (@BTid,
           @DAid)
end
GO
