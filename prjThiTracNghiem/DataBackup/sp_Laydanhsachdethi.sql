USE [dbThiTracNghiem]
GO
/****** Object:  StoredProcedure [dbo].[sp_LayDanhSachDeThi]    Script Date: 12/14/2016 10:27:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[sp_LayDanhSachDeThi]
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