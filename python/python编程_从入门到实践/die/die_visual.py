from die import Die
import pygal

die = Die()
results=[]
for roll_num in range(1000):
	resut = die.roll()
	results.append(resut)

frequencies = []  # 六面分别出现的次数
for value in range(1,die.num_sides+1):
	frequency = results.count(value)
	frequencies.append(frequency)
# print(frequencies)
hist = pygal.Bar()
hist.x_labels = ['1','2','3','4','5','6']  # X轴的值
hist.x_title = "Result"   # X轴的标题
hist.y_title = "Frequency of Result"   # Y轴的标题
hist.add('D6',frequencies)    # Y轴的值
hist.render_to_file('die_visual.svg')    #渲染导出SVG图