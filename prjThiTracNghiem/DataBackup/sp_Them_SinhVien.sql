IF EXISTS(SELECT name FROM sys.objects WHERE name = 'sp_Them_SinhVien')
	DROP PROC sp_Them_SinhVien
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		DzungHT
-- Create date: 15th Dec, 2016
-- Description:	Thêm Sinh viên
-- =============================================
CREATE PROCEDURE sp_Them_SinhVien 
	@Hoten nvarchar(50),
	@NgaySinh nvarchar(50),
	@DiaChi nvarchar(150),
	@KhoaHoc nvarchar(50),
	@TenTaiKhoan nvarchar(50),
	@MatKhau nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION
	BEGIN TRY
		
		INSERT INTO TaiKhoan VALUES(@TenTaiKhoan, @MatKhau, 1, 1)

		INSERT INTO SinhVien VALUES(@Hoten, @NgaySinh, @DiaChi, @KhoaHoc, (SELECT SCOPE_IDENTITY()))

		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
		SELECT ERROR_MESSAGE() AS [ERROR_MESSAGE]
	END CATCH
END
GO
