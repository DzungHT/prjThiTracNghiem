IF EXISTS(SELECT name FROM sys.objects WHERE name = 'sp_Xoa_GiaoVien')
	DROP PROC sp_Xoa_GiaoVien
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		DzungHT
-- Create date: 15th Dec, 2016
-- Description:	Xóa giáo viên
-- =============================================
CREATE PROCEDURE sp_Xoa_GiaoVien 
	@GiaoVienID int, 
	@TaiKhoanID int
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION
	BEGIN TRY
		
		DELETE FROM GiaoVien WHERE GiaoVienID = @GiaoVienID
		DELETE FROM TaiKhoan WHERE TaiKhoanID = @TaiKhoanID

		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
		SELECT ERROR_MESSAGE() AS [ERROR_MESSAGE]
	END CATCH
END
GO
