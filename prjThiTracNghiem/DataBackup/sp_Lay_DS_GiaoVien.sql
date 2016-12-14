IF EXISTS(SELECT name FROM sys.objects WHERE name = 'sp_Lay_DS_GiaoVien')
	DROP PROC sp_Lay_DS_GiaoVien
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		DzungHT
-- Create date: 14th Dec, 2016
-- Description:	Lấy danh sách giáo viên
-- =============================================
CREATE PROCEDURE sp_Lay_DS_GiaoVien 
	-- Add the parameters for the stored procedure here
	@p_TextSearch nvarchar(500),
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
		gv.GiaoVienID as [ID],
		gv.HoTen as [Họ tên giáo viên],
		gv.SDT as [Số điện thoại],
		tk.Username AS [Tên tài khoản]
	FROM 
		GiaoVien gv
	LEFT JOIN TaiKhoan tk ON tk.TaiKhoanID = gv.TaiKhoanID AND tk.Username LIKE @p_TextSearch
	WHERE 
		gv.HoTen LIKE @p_TextSearch OR
		gv.SDT LIKE @p_TextSearch OR
		CAST(gv.GiaoVienID as varchar(4)) Like @p_TextSearch
	ORDER BY gv.GiaoVienID
	OFFSET (@p_pageNumber -1) * @p_pageSize ROWS
	FETCH NEXT @p_pageSize ROWS ONLY

	SELECT @p_pageNumber AS [PageNumber], @p_pageSize as [PageSize], @PageCount as [PageCount]

END
GO
