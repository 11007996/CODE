import pygame.font  # 将文本渲染到屏幕上

class Button(object):
	"""开始按钮"""
	def __init__(self,ai_settings,screen,msg):
		# 初始化按钮
		self.screen = screen
		self.screen_rect = screen.get_rect()
		# 设置按钮的尺寸和其它属性
		self.width,self.height = 130,40
		self.button_color = (0,255,0)
		self.text_color = (255,255,255)
		self.font = pygame.font.SysFont(None,40)   # 默认字体，48号字
		# 创建按钮的rect对象，并居中
		self.rect = pygame.Rect(0,0,self.width,self.height)   # 所谓的按钮就是一个矩形
		self.rect.center = self.screen_rect.center
		# 按钮的标签只创建一次
		self.prep_msg(msg)

	def prep_msg(self,msg):
		# 将msg渲染为图像，并使其在按钮上居中
		self.msg_image = self.font.render(msg,True,self.text_color,self.button_color)
		self.msg_image_rect = self.msg_image.get_rect()
		self.msg_image_rect.center = self.rect.center

	def draw_button(self):
		# 绘制一个用颜色填充的按钮，再绘制文本
		self.screen.fill(self.button_color,self.rect)
		self.screen.blit(self.msg_image,self.msg_image_rect)