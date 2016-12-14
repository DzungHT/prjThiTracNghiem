IF EXISTS(SELECT name FROM sys.objects WHERE name = 'sp_Lay_DS_SinhVien_BaiThi')
	DROP PROC sp_Lay_DS_SinhVien_BaiThi
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
CREATE PROCEDURE sp_Lay_DS_SinhVien_BaiThi
	@p_SinhVienID int,
	@p_pageNumber int,
	@p_pageSize int
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @PageCount int
	SELECT
		@PageCount = 
			(CASE 
				WHEN COUNT(bt.BaiThiID) % @p_pageSize = 0 THEN COUNT(bt.BaiThiID) / @p_pageSize 
				ELSE FLOOR(COUNT(bt.BaiThiID) / @p_pageSize) + 1 
			END) 
	FROM
		BaiThi bt
	INNER JOIN SinhVien sv ON sv.SinhVienID = bt.SinhVienID
	INNER JOIN DeThi dt ON dt.DeThiID = bt.DethiID
	INNER JOIN DotThi dot ON dot.DotThiID = dt.DotThiID

	if @PageCount < @p_pageNumber
		set @p_pageNumber = 1

	SELECT 
		bt.BaiThiID as [ID],

		dot.TenDotThi as [Đợt thi],

		dt.TenDeThi as [Tên đề thi],
		dt.MaDeThi as [Mã đề thi],

		bt.Diem as [Điểm],
		bt.NgayLamBai as [Thời gian bắt đầu làm bài]
	FROM
		BaiThi bt
	INNER JOIN SinhVien sv ON sv.SinhVienID = bt.SinhVienID
	INNER JOIN DeThi dt ON dt.DeThiID = bt.DethiID
	INNER JOIN DotThi dot ON dot.DotThiID = dt.DotThiID
	ORDER BY
		bt.BaiThiID
	OFFSET (@p_pageNumber -1) * @p_pageSize ROWS
	FETCH NEXT @p_pageSize ROWS ONLY

	SELECT @p_pageNumber AS [PageNumber], @p_pageSize as [PageSize], @PageCount as [PageCount]
END
GO
