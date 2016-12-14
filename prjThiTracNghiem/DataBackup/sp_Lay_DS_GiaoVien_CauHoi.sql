IF EXISTS(SELECT name FROM sys.objects WHERE name = 'sp_Lay_DS_GiaoVien_CauHoi')
	DROP PROC sp_Lay_DS_GiaoVien_CauHoi
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		DzungHT
-- Create date: 14th Dec, 2016
-- Description:	Lấy danh sách câu hỏi của giáo viên
-- =============================================
CREATE PROCEDURE sp_Lay_DS_GiaoVien_CauHoi
	@p_GiaoVienID int,
	@p_pageNumber int,
	@p_pageSize int
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @PageCount int
	SELECT @PageCount = (CASE WHEN COUNT(gv.GiaoVienID) % @p_pageSize = 0 THEN COUNT(gv.GiaoVienID) / @p_pageSize ELSE FLOOR(COUNT(gv.GiaoVienID) / @p_pageSize) + 1 END) FROM GiaoVien gv

	if @PageCount < @p_pageNumber
		set @p_pageNumber = 1

	SELECT 
		ch.CauHoiID as [ID],
		chg.TenChuong as [Tên chương],
		hp.TenHocPhan as [Tên học phần],
		dk.TenDoKho AS [Độ khó],
		ch.NoiDung AS [Nội dung]
	FROM 
		CauHoi ch
	INNER JOIN GiaoVien gv ON gv.GiaoVienID = ch.GiaoVienID
	LEFT JOIN Chuong chg ON chg.ChuongID = ch.ChuongID
	LEFT JOIN HocPhan hp ON hp.HocPhanID = chg.HocPhanID
	LEFT JOIN DoKho dk ON dk.DoKhoID = ch.DoKhoID
	WHERE 
		gv.GiaoVienID = @p_GiaoVienID
	ORDER BY ch.CauHoiID
	OFFSET (@p_pageNumber -1) * @p_pageSize ROWS
	FETCH NEXT @p_pageSize ROWS ONLY

	SELECT @p_pageNumber AS [PageNumber], @p_pageSize as [PageSize], @PageCount as [PageCount]
END
GO
