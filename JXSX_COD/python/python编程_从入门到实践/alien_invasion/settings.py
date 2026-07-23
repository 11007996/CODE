class Settings():
	"""存储<外星人入侵>的所有设置的类"""
	def __init__(self):
		# 初始化游戏的设置
		self.screen_width = 1000
		self.screen_height = 600  # 设置游戏窗口大小
		self.bg_color = ( 76, 74, 72)  # 设置背景颜色

		# 子弹设置
		self.bullet_speed_factor = 0.4
		self.bullet_width =3000
		self.bullet_height = 15
		self.bullet_color = 255,200,200
		self.bullets_allowed = 5  # 允许同时出现的子弹数量

		#外星人设置
		self.alien_speed_factor = 1  # 横移速度
		self.fleet_drop_speed = 5   # 下降速度
		self.fleet_direction = 1   # 横移方向，1表示右移，-1表示左移

		# 飞船设置
		self.ship_speed_factor = 1.5   # 设置飞船的移动速度
		self.ship_limit = 3  # 设置飞船剩余存活次数

		self.speedup_scale = 1.1
		self.score_scale = 1.5
		self.initialize_dynamic_settings()


	def initialize_dynamic_settings(self):
		self.ship_speed_factor = 1.5
		self.bullet_speed_factor = 3
		self.alien_speed_factor =1

		self.fleet_direction = 1

		self.alien_points = 50  # 记分

	def increase_speed(self):
		self.ship_speed_factor *= self.speedup_scale
		self.bullet_speed_factor *= self.speedup_scale
		self.alien_speed_factor *= self.speedup_scale
		self.alien_points = int(self.alien_points * self.score_scale)