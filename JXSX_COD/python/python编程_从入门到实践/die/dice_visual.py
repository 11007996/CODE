from die import Die
import pygal

die_1 = Die()  # 同时掷两个骰子
die_2 = Die()
results=[]
for roll_num in range(1000):
	resut = die_1.roll()+die_2.roll()   # 计算两个骰子的和
	results.append(resut)

frequencies = []  # 六面分别出现的次数
max_result = die_1.num_sides + die_2.num_sides
for value in range(2,max_result+1):
	frequency = results.count(value)
	frequencies.append(frequency)

hist = pygal.Bar()
hist.title = "Results of rolling two D6 dice 1000 times."
hist.x_labels = ['2','3','4','5','6','7','8','9','10','11','12']  # X轴的值
hist.x_title = "Result"   # X轴的标题
hist.y_title = "Frequency of Result"   # Y轴的标题
hist.add('D6 + D6',frequencies)    # Y轴的值
hist.render_to_file('dice_visual.svg')    #渲染导出SVG图