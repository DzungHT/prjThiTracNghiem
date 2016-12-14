IF EXISTS(SELECT name FROM sys.objects WHERE name = 'sp_Lay_DS_SinhVien')
	DROP PROC sp_Lay_DS_SinhVien
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		DzungHT
-- Create date: 14th Dec, 2016
-- Description:	Lấy danh sách sinh viên
-- =============================================
CREATE PROCEDURE sp_Lay_DS_SinhVien
	-- Add the parameters for the stored procedure here
	@p_TextSearch nvarchar(500),
	@p_pageNumber int,
	@p_pageSize int
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @PageCount int
	SELECT @PageCount = 
					(CASE 
						WHEN COUNT(sv.SinhVienID) % @p_pageSize = 0 THEN COUNT(sv.SinhVienID) / @p_pageSize 
						ELSE FLOOR(COUNT(sv.SinhVienID) / @p_pageSize) + 1 
					END) 
	FROM SinhVien sv

	if @PageCount < @p_pageNumber
		set @p_pageNumber = 1

	SELECT 
		sv.SinhVienID as [ID],
		sv.HoTen as [Họ tên],
		sv.NgaySinh as [Ngày sinh],
		sv.KhoaHoc as [Khóa học],
		tk.Username as [Tên tài khoản]
	FROM 
		SinhVien sv
	LEFT JOIN TaiKhoan tk ON tk.TaiKhoanID = sv.TaiKhoanID AND tk.Username LIKE @p_TextSearch
	WHERE 
		sv.HoTen LIKE @p_TextSearch OR
		CAST(sv.SinhVienID as varchar(4)) Like @p_TextSearch
	ORDER BY sv.SinhVienID
	OFFSET (@p_pageNumber -1) * @p_pageSize ROWS
	FETCH NEXT @p_pageSize ROWS ONLY

	SELECT @p_pageNumber AS [PageNumber], @p_pageSize as [PageSize], @PageCount as [PageCount]

END
GO
