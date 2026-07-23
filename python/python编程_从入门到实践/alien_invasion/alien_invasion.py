import sys
import pygame
from settings import Settings
from ship import Ship
import game_functions as gf
from pygame.sprite import Group
from alien import Alien
from game_stats import GameStats
from button import Button
from scoreboard import Scoreboard

def run_game():
	# 初始化游戏并创建一个屏幕对象
	pygame.init()
	ai_settings = Settings()
	# 创建一个屏幕对象 ，通过一个元组来控制屏幕大小
	screen = pygame.display.set_mode((ai_settings.screen_width,ai_settings.screen_height))
	# 设置游戏标题
	pygame.display.set_caption("Alien_Invasion")
	# bg_color = (200,200,200)  # 背景颜色，RGB格式

	ship = Ship(ai_settings,screen)  # 实例化飞船
	alien = Alien(ai_settings,screen) # 实例外星人

	bullets = Group() # 创建一个子弹列表

	aliens = Group() # 创建一个外星人列表
	gf.create_fleet(ai_settings,screen,ship,aliens)

	stats = GameStats(ai_settings)  # 游戏的进行状态

	play_button = Button(ai_settings,screen,"Play")

	sb = Scoreboard(ai_settings,screen,stats)

	#开始游戏主循环
	while True:
		gf.check_events(ai_settings,screen,stats,sb,play_button,ship,aliens,bullets)  # 监视键盘和鼠标的事件
		if stats.game_active:
			ship.update()    # 控制飞船移动
			gf.update_bullets(ai_settings,screen,stats,sb,ship,aliens,bullets)  # 控制子弹行为
			gf.update_aliens(ai_settings,screen,stats,sb,ship,aliens,bullets)  # 移动外星人
		gf.update_screen(ai_settings,screen,stats,sb,ship,aliens,bullets,play_button)  # 刷新屏幕



# 执行方法，初始化游戏并开始执行循环		
run_game()