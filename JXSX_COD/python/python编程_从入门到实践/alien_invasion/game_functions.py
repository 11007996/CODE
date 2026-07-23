import sys
import pygame
from bullet import Bullet
from alien import Alien
from time import sleep

def check_events(ai_settings,screen,stats,sb,play_button,ship,aliens,bullets):
		# 监视键盘和鼠标的事件
		for event in pygame.event.get():
			if event.type == pygame.QUIT:
				# 游戏退出
				sys.exit()
			elif event.type == pygame.MOUSEBUTTONDOWN:  #鼠标按键
				mouse_x,mouse_y = pygame.mouse.get_pos()   #获取鼠标位置
				chekc_play_button(ai_settings,screen,stats,sb,play_button,ship,aliens,bullets,mouse_x,mouse_y)
			elif event.type == pygame.KEYDOWN:
				check_keydown_events(event,ai_settings,screen,ship,bullets)
			elif event.type == pygame.KEYUP:
				check_keyup_events(event,ship)

def chekc_play_button(ai_settings,screen,stats,sb,play_button,ship,aliens,bullets,mouse_x,mouse_y):
	button_clicked = play_button.rect.collidepoint(mouse_x,mouse_y)  # 是否点击了按钮
	if button_clicked and not stats.game_active:
		ai_settings.initialize_dynamic_settings()
		pygame.mouse.set_visible(False)  # 隐藏鼠标光标
		# 单击按钮开始游戏
		stats.reset_stats()
		stats.game_active = True   # 游戏开始

		# 重置记分图像
		sb.prep_score()
		sb.prep_high_score()
		sb.prep_level()
		sb.prep_ships()

		aliens.empty()
		bullets.empty()

		create_fleet(ai_settings,screen,ship,aliens)
		ship.center_ship()


def check_keydown_events(event,ai_settings,screen,ship,bullets):
	if event.key == pygame.K_RIGHT:   #按下右箭头就允许飞船一直右移
		ship.moving_right = True
	elif event.key == pygame.K_LEFT:   #按下左箭头就允许飞船一直右移
		ship.moving_left = True
	elif event.key == pygame.K_SPACE:  # 按下空格，新创建一颗子弹
		fire_bullet(ai_settings,screen,ship,bullets)   # 限制子弹数量
	elif event.key == pygame.K_q:   #按Q退出游戏，不区分大小写
		sys.exit()

def check_keyup_events(event,ship):
	if event.key == pygame.K_RIGHT:   #抬起右箭头就不允许飞船右移
		ship.moving_right = False
	elif event.key == pygame.K_LEFT:   #抬起左箭头就不允许飞船右移
		ship.moving_left = False



def update_screen(ai_settings,screen,stats,sb,ship,aliens,bullets,play_button):
	# 设置背景颜色
	screen.fill(ai_settings.bg_color)
	ship.blitme()   #绘制飞船
	# alien.blitme()  #绘制外星人
	aliens.draw(screen)

	for bullet in bullets.sprites():  # 重绘所有的子弹
		bullet.draw_bullet()

	sb.show_score()   # 显示得分

	if not stats.game_active:   # 绘制按钮
		play_button.draw_button()
	# 每个事件重新刷新屏幕
	pygame.display.flip()


def update_bullets(ai_settings,screen,stats,sb,ship,aliens,bullets):   # 更新屏幕上的子弹数量
	bullets.update()
	for bullet in bullets.copy():
		if bullet.rect.bottom <=0:
			bullets.remove(bullet)
	check_bullet_alien_collisions(ai_settings,screen,stats,sb,ship,aliens,bullets)


def check_bullet_alien_collisions(ai_settings,screen,stats,sb,ship,aliens,bullets):
	# 检查子弹是否击中外星人，如果击中就删除相应子弹和外星人
	collisions = pygame.sprite.groupcollide(bullets,aliens,False,True)   # 检查碰撞，返回碰撞的对象，一个字典，键是子弹，值是外星人,第一个True表示删除子弹，第二个True表示删除外星人
	if collisions:
		for aliens in collisions.values():
			stats.score += ai_settings.alien_points * len(aliens)  # 加分
			sb.prep_score()
		check_high_score(stats,sb)
	if len(aliens) == 0:   # 如果外星人都被消灭了，则重新生成一批外星人
		bullets.empty()   # 清空列表
		ai_settings.increase_speed()
		#提高等级
		stats.level += 1
		sb.prep_level()
		create_fleet(ai_settings,screen,ship,aliens)


def fire_bullet(ai_settings,screen,ship,bullets):
	if len(bullets) < ai_settings.bullets_allowed:   # 限制子弹数量
			new_bullet = Bullet(ai_settings,screen,ship)
			bullets.add(new_bullet)


def create_fleet(ai_settings,screen,ship,aliens):
	#创建外星人群
	alien = Alien(ai_settings,screen)
	number_aliens_x = get_number_aliens_x(ai_settings,alien.rect.width)   # 计算一行有多少外星人
	number_rows = get_number_rows(ai_settings,ship.rect.height,alien.rect.height)   # 计算有多少行外星人
	for row_number in range(number_rows):
		for alien_number in range(number_aliens_x):   # 生成多个外星人
			create_alien(ai_settings,screen,aliens,alien_number,row_number)

def get_number_aliens_x(ai_settings,alien_width):
	available_space_x = ai_settings.screen_width -2 * alien_width  # 可用的屏幕宽度
	number_aliens_x = int(available_space_x / (2 * alien_width))   # 屏幕宽度可容纳的外星人数量
	return number_aliens_x

def create_alien(ai_settings,screen,aliens,alien_number,row_number):
	alien = Alien(ai_settings,screen)   # 生成多个外星人
	alien_width = alien.rect.width
	alien.x = alien_width + 2 * alien_width * alien_number
	alien.rect.x = alien.x
	alien.rect.y = alien.rect.height + 2 * alien.rect.height * row_number
	aliens.add(alien)

def get_number_rows(ai_settings,ship_height,alien_height):
	# 计算可容纳多少行外星人
	available_space_y = (ai_settings.screen_height - 3 * alien_height - ship_height)  # 保留3行空白和一行飞船
	number_rows = int(available_space_y / (2 * alien_height))
	return number_rows

def update_aliens(ai_settings,screen,stats,sb,ship,aliens,bullets):   #移动外星人
	# 检查外星人是否处于边缘,改变移动方向
	check_fleet_edges(ai_settings,aliens)
	aliens.update()
	if pygame.sprite.spritecollideany(ship,aliens):   #检测两个编组对象是否发生了碰撞，一旦找到一个就停止遍历
		ship_hit(ai_settings,screen,stats,sb,ship,aliens,bullets)   # 飞船换一条命，子弹和外星人重置
	check_alines_bottom(ai_settings,screen,stats,sb,ship,aliens,bullets)


def check_fleet_edges(ai_settings,aliens):
	# 有外星人到达屏幕边缘则更改运动方向
	for alien in aliens.sprites():
		if alien.check_edges():
			change_fleet_direction(ai_settings,aliens)
			break


def change_fleet_direction(ai_settings,aliens):
	# 有外星人到达屏幕边缘则更改运动方向
	for alien in aliens.sprites():
		alien.rect.y += ai_settings.fleet_drop_speed
	ai_settings.fleet_direction *= -1

def ship_hit(ai_settings,screen,stats,sb,ship,aliens,bullets):
	# 响应被外星人撞到的飞船
	if stats.ships_left >0:
		stats.ships_left -= 1  # 扣一条命
		# 更新记分牌
		sb.prep_ships()
		# 重置子弹和外星人
		aliens.empty()
		bullets.empty()
		create_fleet(ai_settings,screen,ship,aliens)
		ship.center_ship()
		sleep(0.5)
	else:
		stats.game_active = False
		pygame.mouse.set_visible(True)  # 游戏结束重新显示鼠标光标

def check_alines_bottom(ai_settings,screen,stats,sb,ship,aliens,bullets):
	# 检查是否有外星人到达屏幕底部，也会扣飞船一条命
	screen_rect = screen.get_rect()
	for alien in aliens.sprites():
		if alien.rect.bottom >= screen_rect.bottom:
			ship_hit(ai_settings,screen,stats,sb,screen,ship,aliens,bullets)
			break

def check_high_score(stats,sb):
	if stats.score > stats.high_score:
		stats.high_score = stats.score
		sb.prep_high_score()