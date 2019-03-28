

/****** Object:  Table [dbo].[Base_SysLog]    Script Date: 2019/1/10 16:20:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Base_SysLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogType] [varchar](255) NULL,
	[LogContent] [varchar](max) NULL,
	[LogOrigin] [varchar](255) NULL,
	[LogCreateTime] [datetime] NULL,
 CONSTRAINT [PK__Base_Sys__3214EC063FDCCDC3] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'LogType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'LogContent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'LogOrigin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog', @level2type=N'COLUMN',@level2name=N'LogCreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_SysLog'
GO


