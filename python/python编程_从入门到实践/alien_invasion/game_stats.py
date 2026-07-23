class GameStats():
	"""游戏状态统计信息"""
	def __init__(self,ai_settings):
		super(GameStats, self).__init__()
		self.ai_settings = ai_settings
		self.reset_stats()
		self.game_active = False  # 是否开始游戏
		self.high_score =0  # 游戏最高分


	def reset_stats(self):
		self.ships_left = self.ai_settings.ship_limit  # 剩下多少条命
		self.score =0
		self.level =1   #游戏等级