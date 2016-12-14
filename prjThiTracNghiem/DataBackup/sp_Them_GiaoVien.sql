IF EXISTS(SELECT name FROM sys.objects WHERE name = 'sp_Them_GiaoVien')
	DROP PROC sp_Them_GiaoVien
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		DzungHT
-- Create date: 15th Dec, 2016
-- Description:	Thêm giáo viên
-- =============================================
CREATE PROCEDURE sp_Them_GiaoVien 
	@Hoten nvarchar(50),
	@SoDienThoai varchar(50),
	@TenTaiKhoan varchar(50),
	@MatKhau varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION
	BEGIN TRY
		
		INSERT INTO TaiKhoan VALUES(@TenTaiKhoan, @MatKhau, 2, 1)

		INSERT INTO GiaoVien VALUES(@Hoten, @SoDienThoai, (SELECT SCOPE_IDENTITY()))

		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
		SELECT ERROR_MESSAGE() AS [ERROR_MESSAGE]
	END CATCH
END
GO
