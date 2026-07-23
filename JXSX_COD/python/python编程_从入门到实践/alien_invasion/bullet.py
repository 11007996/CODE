import pygame
from pygame.sprite import Sprite   # 可用于对游戏中的对象进行编组，便于统一处理

class Bullet(Sprite):
	"""继承Sprite类"""
	def __init__(self, ai_settings,screen,ship):
		super(Bullet, self).__init__()
		self.screen = screen

		# 先在原点创建一个子弹对象，再放到飞船的位置
		self.rect = pygame.Rect(0,0,ai_settings.bullet_width,ai_settings.bullet_height)
		self.rect.centerx = ship.rect.centerx
		self.rect.top = ship.rect.top
		
		self.y = float(self.rect.y)
		self.color = ai_settings.bullet_color
		self.speed_factor = ai_settings.bullet_speed_factor


	def update(self):
		# 向上移动子弹
		self.y -= self.speed_factor
		self.rect.y = self.y

	def draw_bullet(self):
		# 绘制子弹
		pygame.draw.rect(self.screen,self.color,self.rect)