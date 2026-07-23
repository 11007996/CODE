import pygame
from pygame.sprite import Sprite

class Ship(Sprite):
	"""飞船类"""
	def __init__(self,ai_settings, screen):
		"""初始化飞船并设置其初始位置"""
		super(Ship,self).__init__()
		self.screen = screen
		self.ai_settings = ai_settings
		# 加载飞船图像并获取其外接矩形
		self.image = pygame.image.load("img/spaceship.png")
		self.rect = self.image.get_rect()  # 飞船的外接矩形
		self.screen_rect = screen.get_rect()  #屏幕的外接矩形
		#将每艘飞船放在屏幕底部中央
		self.rect.centerx = self.screen_rect.centerx  # X轴的中心点，center表示屏幕中央，centery表示y轴的中心点
		self.rect.bottom = self.screen_rect.bottom   #屏幕的底部，top,left,right
		self.center =float(self.rect.centerx)

		self.moving_right = False   # 允许飞船右移的标志
		self.moving_left = False   # 允许飞船右移的标志


	def blitme(self):
	    # 在指定位置绘制飞船
		self.screen.blit(self.image,self.rect)


	def update(self):
		if self.moving_right and self.rect.right < self.screen_rect.right:
			self.center += self.ai_settings.ship_speed_factor
		elif self.moving_left and self.rect.left >0:
			self.center -= self.ai_settings.ship_speed_factor
		self.rect.centerx = self.center

	def center_ship(self):
		# 让飞船在屏幕上居中
		self.center = self.screen_rect.centerx