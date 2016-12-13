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
	@p_TextSearch nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		gv.GiaoVienID,
		gv.HoTen,
		gv.SDT,
		tk.Username 
	FROM 
		GiaoVien gv
	INNER JOIN TaiKhoan tk ON tk.TaiKhoanID = gv.TaiKhoanID AND tk.Username LIKE @p_TextSearch
	WHERE 
		gv.HoTen LIKE @p_TextSearch OR
		gv.SDT LIKE @p_TextSearch OR
		CAST(gv.GiaoVienID as varchar(4)) Like @p_TextSearch

END
GO
