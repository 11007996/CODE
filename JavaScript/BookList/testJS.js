/*var di = document.getElementById("ppp");
di.addEventListener("mouseenter",function(){
	di.style.cssText = "width: 100px;height: 100px;background: blue;";
	console.log("123");
});
di.addEventListener("mouseleave",aaa);
function aaa()
{
	di.style.cssText = "width: 100px;height: 100px;background: red;";

	try
	{
			console.log(a);
	}
	catch(error)
	{
		console.log("发生错误");
	}
	
}*/

//返回数据类型
/*let a;
console.log(typeof a);
console.log(typeof 11);
console.log(typeof true);
console.log(typeof null);*/


/*let s1 = new String("some text");
let s2 = s1.substring(0,2);
s1 = null;
console.log(s2);*/

//检查数据类型,判断对象是否为指定的类型
/*let obj = new Object(1);
console.log(obj instanceof Number);
console.log(obj instanceof Array);*/


// 判断value是否为数组
// Array.isArray(value)

//Number类型重写toString方法返回相应的进制
/*let num = 10;
console.log(num.toString()); // "10"
console.log(num.toString(2)); // "1010"
console.log(num.toString(8)); // "12"
console.log(num.toString(10)); // "10"
console.log(num.toString(16)); // "a"*/

//保留2位小数,四舍五入
/*let num = 10.136;
console.log(num.toFixed(2)); */

//科学计数法
/*let num = 123000000000000;
console.log(num.toExponential());
console.log(num.toExponential(3));*/

//自动选择合适的格式返回数值
/*let num = 99;
console.log(num.toPrecision());
console.log(num.toPrecision(1)); // "1e+2"
console.log(num.toPrecision(2)); // "99"
console.log(num.toPrecision(3)); // "99.0"*/

//判断是否为整数
/*console.log(Number.isInteger(1)); // true
console.log(Number.isInteger(1.00)); // true
console.log(Number.isInteger(1.01)); // false*/

/*let message = "abcde";
console.log(message.length);
console.log(message.charAt(2));  //第2位的字符
console.log(message.charCodeAt(2));   //第2位的字符编码
console.log(String.fromCharCode(0x61, 0x62, 0x63, 0x64, 0x65));  //UTF-16码元生成字符串
console.log(message.codePointAt(2));      //第2位的字符编码
console.log(String.fromCharCode(97, 98, 55357, 56842, 100, 101)); // ab☺de
console.log(String.fromCodePoint(97, 98, 128522, 100, 101));
console.log(message.slice(-3)); //倒序截取字符*/


/*let stringValue = "Loreem ipsum dolor sit amet, consectetur adipisicing elit";
let positions = new Array();
let pos = stringValue.indexOf("e");
while(pos > -1) {
positions.push(pos);//将值插入列表中
pos = stringValue.indexOf("e", pos + 1);//查找下一个e出现的位置
}
console.log(positions); // [3,24,32,35,52]*/


//是否包含指定的字符
/*let message = "foobarbaz";
console.log(message.startsWith("foo")); // true
console.log(message.startsWith("bar")); // false
console.log(message.endsWith("baz")); // true
console.log(message.endsWith("bar")); // false
console.log(message.includes("bar")); // true
console.log(message.includes("qux"));*/


/*let stringValue = " hello world ";
let trimmedStringValue0 = stringValue.trim();
let trimmedStringValue1 = stringValue.trimLeft();
let trimmedStringValue2 = stringValue.trimRight();
console.log(trimmedStringValue0);
console.log(trimmedStringValue1);
console.log(trimmedStringValue2);*/

//填充字符到指定长度
/*let stringValue = "foo";
console.log(stringValue.padStart(6)); // " foo"
console.log(stringValue.padStart(9, ".")); // "......foo"   用.把字符补充到9位长度
console.log(stringValue.padEnd(7,"6")); // "foo "
console.log(stringValue.padEnd(9, ".")); // "foo......"*/



/*let message = "abc";
let stringIterator = message[Symbol.iterator]();
console.log(stringIterator.next()); // {value: "a", done: false}
console.log(stringIterator.next()); // {value: "b", done: false}
console.log(stringIterator.next()); // {value: "c", done: false}
console.log(stringIterator.next());*/

//解析为数组
/*let message = "abcde";
console.log([...message]);*/


/*let text = "cat, bat, sat, fat";
let result = text.replace("at", "ond");
console.log(result); // "cond, bat, sat, fat"
*/

/*let text = "cat, bat, sat, fat";
result = text.replace(/(.at)/g, "word ($1)");
console.log(result); // word (cat), word (bat), word (sat), word (fat)*/


/*function htmlEscape(text) {
return text.replace(/[<>"&]/g, function(match, pos, originalText) {
switch(match) {
case "<":
return "&lt;";
case ">":
return "&gt;";
case "&":
return "&amp;";
case "\"":
return "&quot;";
}
});
}
console.log(htmlEscape("<p class=\"greeting\">Hello world!</p>"));
// "&lt;p class=&quot;greeting&quot;&gt;Hello world!</p>"*/


/*let colorText = "red,blue,green,yellow";
let colors1 = colorText.split(","); // ["red", "blue", "green", "yellow"]
let colors2 = colorText.split(",", 2); // ["red", "blue"]
let colors3 = colorText.split(/[^,]+/); // ["", ",", ",", ",", ""]
for(let a of colors1)
{
	console.log(a);
}
console.log(colors1);
console.log(colors2);
console.log(colors3);*/


//以字符编码顺序比较大小
/*let stringValue = "b";
console.log(stringValue.localeCompare("a")); // 1
console.log(stringValue.localeCompare("b")); // 0
console.log(stringValue.localeCompare("c")); // -1*/

//解析为uri
/*let uri = "http://www.wrox.com/illegal value.js#start";
// "http://www.wrox.com/illegal%20value.js#start"
console.log(encodeURI(uri));
// "http%3A%2F%2Fwww.wrox.com%2Fillegal%20value.js%23start"
console.log(encodeURIComponent(uri));*/

//执行HTML语句
//eval("console.log('hi')");

//匿名方法
/*let global = function() {
return 12;
}();
console.log(global);*/

// 最大最小值
/*let max = Math.max(3, 54, 32, 16);
console.log(max); // 54
let min = Math.min(3, 54, 32, 16);
console.log(min); // 3*/

// 四舍五入
/*console.log(Math.ceil(25.01)); // 26
console.log(Math.round(25.9)); // 26
console.log(Math.floor(25.9)); // 25*/

// 随机数
/*let num = Math.floor(Math.random() * 10 + 1);
console.log(num);*/

//对象字面量表示法
/*function displayInfo(args) {
let output = "";
if (typeof args.name == "string"){
output += "Name: " + args.name + "\n";
}
if (typeof args.age == "number") {
output += "Age: " + args.age + "\n";
}
alert(output);
}
// let args = {"name":"Nicholas","age": 29};
displayInfo({
name: "Nicholas",
age: 29
});
displayInfo({
name: "Greg"
});*/

//属性用参数调用
/*let args = {"name":"Nicholas","age": 29};
console.log(args["name"]);
console.log(args.age);*/

//实例数组
// let colors = new Array(10);
// let colors = new Array("red", "blue", "green");
/*let a = [1,2,3,4,5,6];
for (let i of a)
{
	console.log(i);
}*/

//from()用于将类数组结构转换为数组实例
// console.log(Array.from("Matt")); // ["M", "a", "t", "t"]

/*const iter = {
*[Symbol.iterator]() {
yield 1;
yield 2;
yield 3;
yield 4;
}
};
console.log(Array.from(iter)); // [1, 2, 3, 4]*/


/*const a1 = [1, 2, 3, 4];
//from第一个参数是类对象，第二个参数是增强函数，第三个参数是增强函数的this值
const a2 = Array.from(a1, x => x**2);
const a3 = Array.from(a1, function(x) {return x**this.exponent}, {exponent: 3});
console.log(a2); // [1, 4, 9, 16]
console.log(a3); // [1, 4, 9, 16]*/

//of用于将一组参数转换为数组实例
// console.log(Array.of(1, 2, 3, 4)); // [1, 2, 3, 4]

//数组用空调占位
/*const options = [1,,,,5];
for (const option of options) 
{
    console.log(option === undefined);
}
console.log(options.map(x => x>2));
console.log(options.join('-'));*/

//map函数对调用方法进行处理后再返回，对数组元素依次进行处理
// array.map(function(currentValue,index,arr), thisValue)


/*let a = [];
a[0] = "qqq";
a[1] = "bbb";
console.log(a.length);
a.length = 5;
console.log(a[0]);
console.log(a.length);
*/

// 返回数组索引、返回数组值
/*const a = ["foo", "bar", "baz", "qux"];
// 因为这些方法都返回迭代器，所以可以将它们的内容
// 通过Array.from()直接转换为数组实例
const aKeys = Array.from(a.keys());     //返回数组索引的数组
const aValues = Array.from(a.values());   //返回数组值的数组
const aEntries = Array.from(a.entries());    //返回数组索引和值的二维数组
console.log(aKeys); // [0, 1, 2, 3]
console.log(aValues); // ["foo", "bar", "baz", "qux"]
console.log(aEntries); // [[0, "foo"], [1, "bar"], [2, "baz"], [3, "qux"]]
for (const [idx, element] of aEntries) {
console.log(idx);
console.log(element);
}
*/

/*
//fill填充数组
const zeroes = [0, 0, 0, 0, 0];
// 用5 填充整个数组
zeroes.fill(5);
console.log(zeroes); // [5, 5, 5, 5, 5]
zeroes.fill(0); // 重置
// 用6 填充索引大于等于3 的元素
zeroes.fill(6, 3);
console.log(zeroes); // [0, 0, 0, 6, 6]
zeroes.fill(0); // 重置
// 用7 填充索引大于等于1 且小于3 的元素
zeroes.fill(7, 1, 3);
console.log(zeroes); // [0, 7, 7, 0, 0];
zeroes.fill(0); // 重置
// 用8 填充索引大于等于1 且小于4 的元素
// (-4 + zeroes.length = 1)
// (-1 + zeroes.length = 4)
zeroes.fill(8, -4, -1);
console.log(zeroes); // [0, 8, 8, 8, 0];
//fill()静默忽略超出数组边界、零长度及方向相反的索引范围：
const zeroes = [0, 0, 0, 0, 0];
// 索引过低，忽略
zeroes.fill(1, -10, -6);
console.log(zeroes); // [0, 0, 0, 0, 0]
// 索引过高，忽略
zeroes.fill(1, 10, 15);
console.log(zeroes); // [0, 0, 0, 0, 0]
// 索引反向，忽略
zeroes.fill(2, 4, 2);
console.log(zeroes); // [0, 0, 0, 0, 0]
// 索引部分可用，填充可用部分
zeroes.fill(4, 3, 10)
console.log(zeroes); // [0, 0, 0, 4, 4]
*/

//堆栈
/*
let colors = new Array(); // 创建一个数组
let count = colors.push("red", "green"); // 推入两项
console.log(count); // 2
count = colors.push("black"); // 再推入一项
console.log(count); // 3
let item = colors.pop(); // 取得最后一项并删除此项
console.log(item); // black
console.log(colors.length); //
let a = colors.shift(); // 取得第一项并删除此项
console.log(a); // red
console.log(colors.length);
let counts = colors.unshift("red", "green"); //从数组开头推入两项
console.log(counts);
colors.reverse();   //数组顺序反转
colors.sort();   //升序排列数组
console.log([...colors]);     //返回数组的值
console.log(Array.from(colors.values()));     //返回数组的值 
console.log(colors.valueOf());   //返回数组的值 
*/


//数组连接
/*
let colors = ["red", "green", "blue"];
let colors2 = colors.concat("yellow", ["black", "brown"]);
console.log(colors); // ["red", "green","blue"]
console.log(colors2); // ["red", "green", "blue", "yellow", "black", "brown"]
*/


// 截取数组的元素
/*
let colors = ["red", "green", "blue", "yellow", "purple"];
let colors2 = colors.slice(1);
let colors3 = colors.slice(1, 4);
console.log(colors2); // green,blue,yellow,purple
console.log(colors3); // green,blue,yellow
*/



/*let colors = ["red", "green", "blue"];
//第一个参数表示从第几位元素开始，第二个参数表示要删除几个元素，之后的参数表示要插入的元素
let removed = colors.splice(0,1); // 删除第一项
console.log(colors); // green,blue
console.log(removed); // red，只有一个元素的数组
removed = colors.splice(1, 0, "yellow", "orange"); // 在位置1 插入两个元素
console.log(colors); // green,yellow,orange,blue
console.log(removed); // 空数组
removed = colors.splice(1, 1, "red", "purple"); // 插入两个值，删除一个元素
console.log(colors); // green,red,purple,orange,blue
console.log(removed);*/


//find返回匹配到的第一个元素
/*let a = [1,2,3,4,5,6,7,8,9,0];
console.log(a.find(x => x > 6));
//findIndex返回匹配到的第一个元素的索引
console.log(a.findIndex(x => x > 6));*/


/*
let numbers = [1, 2, 3, 4, 5, 4, 3, 2, 1];
let everyResult = numbers.every((item, index, array) => item > 2);      //所有true才返回true
let everyResult = numbers.every((item, index, array) => item > 2);      //一个元素为true就返回true
let filterResult = numbers.filter((item, index, array) => item > 2);    //返回匹配的元素
let mapResult = numbers.map((item, index, array) => item * 2);          //执行增强方法后再返回数组
numbers.forEach((item, index, array) => {return 111});              //执行方法返回数组
console.log(everyResult); // false

*/



//4个参数：上一个归并值、当前项、当前项的索引和数组本身
//reduce从第一个元素到最后一个元素遍历，reduceRight从最后的一个元素到第一个元素遍历，两个方法只是顺序不同
/*let values = [1, 2, 3, 4, 5];
let sum = values.reduce((prev, cur, index, array) => prev + cur);
let sum1 = values.reduceRight(function(prev, cur, index, array){return prev + cur;});
console.log(sum); // 15
console.log(sum1);*/



/*const buf = new ArrayBuffer(1);
const view = new DataView(buf);
view.setInt8(0, 2.8);
alert(view.getInt8(0)); // 1
view.setInt8(0, [4]);
alert(view.getInt8(0)); // 4
view.setInt8(0, 'f');
alert(view.getInt8(0)); // 0
view.setInt8(0, Symbol())*/


//Set集合
/*const s = new Set();
console.log(s.has("Matt")); // false
console.log(s.size); // 0
s.add("Matt")
.add("Frisbie");
console.log(s.has("Matt")); // true
console.log(s.size); // 2
s.delete("Matt");
console.log(s.has("Matt")); // false
console.log(s.has("Frisbie")); // true
console.log(s.size); // 1
s.clear(); // 销毁集合实例中的所有值
console.log(s.has("Matt")); // false
console.log(s.has("Frisbie")); // false
console.log(s.size);*/

/*
console.log('%d + %d = %d',1,1,2);
console.error('输出错误信息，会以红色显示');
console.warn('打印警告信息，会以黄色显示');
console.info('打印一般信息');

console.assert(false,'判断为false才显示的信息');//console.assert(bool,”info”) 如果bool为false 打印出info 否则不打印

console.table([['中国','美国'],['好']]);//传入的对象或数组以表格方式显示

function fn(){ console.trace(); }//打印 调用链 fn2()调用fn1()，fn1()调用fn()
function fn1(){ fn(); }
function fn2(){ fn1(); }
fn2();

console.log('%o',document.body);
console.log('%O',document.body);


//显示调用时间
console.time();
    for(var i=0;i<100000;i++){
        var j=i*i;
    }
    console.timeEnd();

//统计代码或函数被调用了多少次
    var fn_ = function(){ console.count('hello world'); }
    for(var i=0;i<5;i++){
        fn_();
    }

console.group('分组1');
    console.log('语文');
    console.log('数学');
        console.group('其他科目');
        console.log('化学');
        console.log('地理');
        console.log('历史');
        console.groupEnd('其他科目');
    console.groupEnd('分组1');
    */

/*function drawDiagonal(){
    var canvas = document.getElementById("can");
    if(canvas.getContext)
    {
        var context = canvas.getContext("2d");
    context.beginPath();
    context.moveTo(70,140);
    context.lineTo(140,70);
    context.stroke();
    }
    else
    {
        console.log(1111);
    }
}
window.addEventListener("load",drawDiagonal,true);*/


/*let collection = ['foo', 'bar', 'baz'];
collection.forEach((item) => console.log(item));*/


/*let collection = ['foo', 'bar', 'baz'];
collection.forEach((item) => console.log(item));
let iter = arr[Symbol.iterator]();
console.log(iter); // ArrayIterator {}
// 执行迭代
console.log(iter.next()); // { done: false, value: 'foo' }
console.log(iter.next()); // { done: false, value: 'bar' }
console.log(iter.next()); // { done: true, value: undefined }*/

/*
let arr = ['foo','asght'];
let iter = arr[Symbol.iterator]();
console.log(iter.next()); // { done: false, value: 'foo' }
console.log(iter.next()); // { done: true, value: undefined }
console.log(iter.next()); // { done: true, value: undefined }
console.log(iter.next()); // { done: true, value: undefined }*/


/*class Foo 
{
    [Symbol.iterator]() 
    {
        return {
                next() 
                    {
                        return { done: false, value: 'foo' };
                    }
                }
    }
}
let f = new Foo();
// 打印出实现了迭代器接口的对象
console.log(f[Symbol.iterator]()); // { next: f() {} }
// Array 类型实现了可迭代接口（Iterable）
// 调用Array 类型的默认迭代器工厂函数
// 会创建一个ArrayIterator 的实例
let a = new Array();
// 打印出ArrayIterator 的实例
console.log(a[Symbol.iterator]()); // Array Iterator {}*/


/*function* generatorFn() {
yield* [1, 2, 3];
}
let generatorObject = generatorFn();
for (const x of generatorFn()) {
console.log(x);
}*/


/*//修改对象的属性
let person = {};
person.name = "AAB";
console.log(person.name); // "AAB"
//3个参数：对象名，属性名，属性的对象特性(writable是否可写，value属性值，Enumerable是否可迭代，Configurable是否可删除再修改)
Object.defineProperty(person, "name", {
writable: true,
value: "Nicholas"
});
Object.defineProperty(person, "age", {
writable: false,
value: "20"
});
console.log(person.name); // "Nicholas"
person.name = "Greg";
console.log(person.name); // "Greg"
person.age =18;
console.log(person.age);  // "20"*/


/*//set get 访问器
let book = {
year_: 2017,
edition: 1
};
Object.defineProperty(book, "year", {
get() {
return this.year_;
},
set(newValue) {
if (newValue > 2017) {
this.year_ = newValue;
this.edition += newValue - 2017;
}
}
});
book.year = 2018;
console.log(book.edition); // 2*/

//访问器设置多个属性
/*let book = {};
Object.defineProperties(book, {
year_: {
value: 2019
},
edition_: {         //下划线表示不能被外部访问，只能通过get set访问器修改属性值
writable: true,
value: 1
},
year: {
get() {
return this.year_;
 },
 set(newValue) {
if (newValue > 2017) {
 this.year_ = newValue;
 this.edition += newValue - 2017;
}
 }
 },
 edition: {
    get(){ return this.edition_;},
    set(nvalue){ this.edition_ = nvalue;}
 }
 }
);
console.log(book.edition); 
console.log(book.year); */


/*console.log(true === 1); // false
console.log({} === {}); // false
console.log(2 === 2);
console.log(+0 === -0); // true
console.log(+0 === 0); // true
console.log(-0 === 0);
console.log(NaN === NaN); // false
console.log(isNaN(NaN)); // true
console.log(Object.is(+0, -0)); // false
console.log(Object.is(+0, 0)); // true
console.log(Object.is("abcc", "abc"));*/


//动态属性赋值
/*const nameKey = 'name';
const ageKey = 'age';
const jobKey = 'job';
let uniqueToken = 0;
function getUniqueKey(key) {
return `${key}_${uniqueToken++}`;
}
let person = {
[getUniqueKey(nameKey)]: 'Matt',
[getUniqueKey(ageKey)]: 27,
[getUniqueKey(jobKey)]: 'Software engineer'
};
console.log(person);*/



/*let name = 'Matt';
let person = {
name   //等价于name:name，属性名和变量名一致时可简写
};
console.log(person); // { name: 'Matt' }*/

/*let person = {
sayName: function(name) {          //对象内含方法
console.log(`My name is ${name}`);
}
};
person.sayName('Matt');
//对象内方法可简写为：
let person = {
sayName(name) {
console.log(`My name is ${name}`);
}
};
person.sayName('Matt');
//变量作为方法名
const methodKey = 'sayName';
let person = {
[methodKey](name) {
console.log(`My name is ${name}`);
}
}
person.sayName('Matt'); // My name is Matt*/


//对象解构
/*let person = {
name: 'Matt',
age: 27
};
let { name: personName, age: personAge } = person;
console.log(personName); // Matt
console.log(personAge); // 27

let person = {
name: 'Matt',
age: 27
};
let { name, age } = person;
console.log(name); // Matt
console.log(age); // 27

let {length} ="abcde";
console.log(length);  //解构toObject的length属性
let { constructor: c } = 4;     //解构toObject的对象类型属性，constructor用来标识对象类型
console.log(c === Number); // true

let personName, personAge;
let person = {
name: 'Matt',
age: 27
};
({name: personName, age: personAge} = person);    //变量提前声明了必须用括号括起来
console.log(personName, personAge); // Matt, 27*/

//工厂模式创建对象
/*function createPerson(name, age, job) {
let o = new Object();
o.name = name;
o.age = age;
o.job = job;
o.sayName = function() {
console.log(this.name);
};
return o;
}
let person1 = createPerson("Nicholas", 29, "Software Engineer");
let person2 = createPerson("Greg", 27, "Doctor");
person2.sayName();*/


//构造函数创建对象
/*let Person = function(name, age, job) 
{
    this.name = name;
    this.age = age;
    this.job = job;
    this.sex = "male";
    this.sayName = function() 
    {
        console.log(this.name);
    };
}
let person1 = new Person("Nicholas", 29, "Software Engineer");
let person2 = new Person("Greg", 27, "Doctor");
person1.sayName(); // Nicholas
person2.sayName(); // Greg
console.log(person2.sex);
console.log(person1 instanceof Object); // true
console.log(person1 instanceof Person); // true
console.log(person2 instanceof Object); // true
console.log(person2 instanceof Person); // true*/

//原型模式创建对象
/*function Person(name,age,job) {}
Person.prototype.name = 'name';
Person.prototype.age = 'age';
Person.prototype.job = 'job';
Person.prototype.sayName = function() {
console.log(this.name);
};
let person1 = new Person();
person1.sayName();
let person2 = new Person();
person2.sayName();
console.log(person1.sayName == person2.sayName); // true
console.log(Person.prototype.isPrototypeOf(person1));      //判断实例与构造函数是否为同一原型
console.log(Object.getPrototypeOf(person1).name);      //通过原型获取属性值*/


// 不能直接改变原型的属性值，但可以通过实例的属性值覆盖原型的属性值
/*function Person() {}
Person.prototype.name = "Nicholas";
Person.prototype.age = 29;
Person.prototype.job = "Software Engineer";
Person.prototype.sayName = function() {
console.log(this.name);
};
let person1 = new Person();
let person2 = new Person();
person1.name = "Greg";
console.log(person1.name); // "Greg"，来自实例
console.log(person2.name); // "Nicholas"，来自原型
console.log(person1.hasOwnProperty("name"));   //判断属性值是来自实例还是原型*/


/*function Person() {}
Person.prototype.name = "Nicholas";
Person.prototype.age = 29;
Person.prototype.job = "Software Engineer";
Person.prototype.sayName = function() {
console.log(this.name);
};
let person1 = new Person();
let person2 = new Person();
console.log("name" in person1);    //in用于属性是否在对象内，即时是否可调用到对应属性*/


/*let k1 = Symbol('k1'), k2 = Symbol('k2');
let o = {
1: 1,
first: 'first',
[k1]: 'sym2',
second: 'second',
0: 0
};
o[k2] = 'sym2';
o[3] = 3;
o.third = 'third';
o[2] = 2;
console.log(Object.getOwnPropertyNames(o));
// ["0", "1", "2", "3", "first", "second", "third"]
console.log(Object.getOwnPropertySymbols(o));
// [Symbol(k1), Symbol(k2)]*/


/*const o = {
foo: 'bar',
baz: 1,
qux: {}
};
console.log(Object.values(o));  //返回对象的属性值组成一个数组
console.log(Object.entries(o));  //返回对象的属性名及属性值组成一个二维数组*/


//通过原型重写对象的原生
/*String.prototype.startsWith = function (text) {      
return this.indexOf(text) === 0;
};
let msg = "Hello world!";
console.log(msg.startsWith("Hello")); // true*/


//原型链实现对象继承
/*function SuperType() {
this.property = true;
}
SuperType.prototype.getSuperValue = function() {
return this.property;
};


function SubType() {
this.subproperty = false;
}


// 继承SuperType
SubType.prototype = new SuperType();
SubType.prototype.getSubValue = function () {
    return this.subproperty;
};


let instance = new SubType();
console.log(instance.getSuperValue()); // true
*/


//原型链实现对象继承并拓展方法，继承所有新建对象共享属性值和方法
/*function SuperType() {
this.property = true;
}
SuperType.prototype.getSuperValue = function() {
return this.property;
};
function SubType() {
this.subproperty = false;
}
// 继承SuperType
SubType.prototype = new SuperType();
// 新方法
SubType.prototype.getSubValue = function () {
return this.subproperty;
};
//覆盖已有的方法
SubType.prototype.getSuperValue = function () {
return false;
};
//继承后新增方法
SubType.prototype.getS = function () {
return 3;
};
let instance = new SubType();
let a = new SuperType();
console.log(a.getSuperValue());
console.log(instance.getSuperValue()); // false
console.log(instance.getS());*/


//盗用构造函数，避免了继承子类对象间的属性共享
/*function SuperType() {
this.colors = ["red", "blue", "green"];
}
function SubType() {
// 继承SuperType
// SuperType.call(this);   //子类中调用父类的构造函数
SuperType.apply(this); 
}
let instance1 = new SubType();
instance1.colors.push("black");
console.log(instance1.colors); // "red,blue,green,black"
let instance2 = new SubType();
console.log(instance2.colors); // "red,blue,green"*/

//盗用构造函数，并传参
/*function SuperType(name){
this.name = name;
}
function SubType() {
// 继承SuperType 并传参
SuperType.call(this, "Nicholas");
// 实例属性
this.age = 29;
}
let instance = new SubType();
console.log(instance.name); // "Nicholas";
console.log(instance.age); // 29*/




//call调用可控个数参数，apply调用不明个数参数
/*let obj1 = {
    name: "张三",
    age: 24,
    user(args) {
      console.log("姓名：", this.name);
      console.log("年龄：", this.age);
      console.log("参数", args);
    }
  }
  let obj2 = {
    name: "李四",
    age: 30,
    user: function (args) {
      console.log("姓名：", this.name);
      console.log("年龄：", this.age);
      console.log("参数", args);
    }
  }
  // 正常的调用
  obj1.user('我是参数');
  obj2.user('我是参数');
  obj1.user.call(obj2, "我是参数"); // 使用call方法改变
  obj2.user.apply(obj1, ["我是参数"]);*/



//组合继承
/*function SuperType(name){
this.name = name;
this.colors = ["red", "blue", "green"];
}
SuperType.prototype.sayName = function() {
console.log(this.name);
};
function SubType(name, age){
// 继承属性
SuperType.call(this, name);    //盗用父类构造函数继承，拓展子类属性
this.age = age;
}
//继承方法
SubType.prototype = new SuperType();     //原型链继承，继承父类原型
SubType.prototype.sayAge = function() {
console.log(this.age);
};
let instance1 = new SubType("Nicholas", 29);
instance1.colors.push("black");
console.log(instance1.colors); // "red,blue,green,black"
instance1.sayName(); // "Nicholas";
instance1.sayAge(); // 29
let instance2 = new SubType("Greg", 27);
console.log(instance2.colors); // "red,blue,green"
instance2.sayName(); // "Greg";
instance2.sayAge(); // 27
*/


// 原型式继承，在一个对象的基础上新建另一个对象并修改
/*function object(o) {
function F() {}
F.prototype = o;
return new F();
}

let person = {
name: "Nicholas",
friends: ["Shelby", "Court", "Van"]
};
let anotherPerson = object(person);
anotherPerson.name = "Greg";
anotherPerson.friends.push("Rob");
let yetAnotherPerson = object(person);
yetAnotherPerson.name = "Linda";
yetAnotherPerson.friends.push("Barbie");
console.log(person.friends); // "Shelby,Court,Van,Rob,Barbie"*/



//寄生式继承，新建对象副本，再修改副本
/*function object(o) {
function F() {}
// F.prototype = o;
return new F();
}

function createAnother(original){
let clone = object(original); // 通过调用函数创建一个新对象
clone.sayHi = function() { // 以某种方式增强这个对象
console.log("hi");
};
return clone; // 返回这个对象
}

let person = {
name: "Nicholas",
friends: ["Shelby", "Court", "Van"]
};
let anotherPerson = createAnother(person);
anotherPerson.sayHi(); // "hi"*/



//寄生式组合继承，结合原型链
/*function SuperType(name) {
this.name = name;
this.colors = ["red", "blue", "green"];
}
SuperType.prototype.sayName = function() {
console.log(this.name);
};
function SubType(name, age){
SuperType.call(this, name); // 第二次调用SuperType(),盗用构造函数
this.age = age;
}
SubType.prototype = new SuperType(); // 第一次调用SuperType()
SubType.prototype.constructor = SubType;
SubType.prototype.sayAge = function() {
console.log(this.age);
};
let a = new SubType("aaa",12);
a.sayAge();*/


//class类
/*class Animal {}
class Person {
constructor() {      //constructor关键字表示构造函数
console.log('person ctor');
}
}
class Vegetable {
constructor() {
this.color = 'orange';
this.dd = function(test)
{
    return test;
}
}
}
let a = new Animal();
let p = new Person(); // person ctor
let v = new Vegetable();
console.log(v.color); // orange
console.log(v.dd(1121));
*/


//类可以在任何地方定义，此例在数组中定义
/*let classList = 
[
class 
{
    constructor(id) 
    {
        this.id_ = id;
        console.log(`instance ${this.id_}`);
    }
}
];

function createInstance(classDefinition, id) {
    return new classDefinition(id);
}

let foo = createInstance(classList[0], 3141); // instance 3141*/





//两个实例的属性不一致
/*class Person {
constructor() {
// 这个例子先使用对象包装类型定义一个字符串
// 为的是在下面测试两个对象的相等性
this.name = new String('Jack');
this.sayName = () => console.log(this.name);
this.nicknames = ['Jake', 'J-Dog']
}
}
let p1 = new Person(),
p2 = new Person();
p1.sayName(); // Jack
p2.sayName(); // Jack
console.log(p1.name === p2.name); // false
console.log(p1.sayName === p2.sayName); // false
console.log(p1.nicknames === p2.nicknames); // false
p1.name = p1.nicknames[0];
p2.name = p2.nicknames[1];
p1.sayName(); // Jake
p2.sayName(); // J-Dog*/


//类方法通过原型调用
/*class Person {
constructor() {
// 添加到this 的所有内容都会存在于不同的实例上
this.locate = () => console.log('instance');
}
locate() {    //构造函数外定义函数，用原型进行调用
console.log('prototype');
}
}
let p = new Person();
p.locate(); // instance   //构造函数通过实例调用
Person.prototype.locate(); // prototype，通过用原型进行调用*/



//类访问器
/*class Person {
set name(newName) {
this.name_ = newName;
}
get name() {
return this.name_;
}
}
let p = new Person();
p.name = 'Jake';
console.log(p.name); // Jake*/


//类构造函数，原型函数，静态函数
/*class Person {
constructor() {
// 添加到this 的所有内容都会存在于不同的实例上
this.locate = () => console.log('instance', this);
}
// 定义在类的原型对象上
locate() {
console.log('prototype', this);
}
// 定义在类本身上静态方法
static locate() {
console.log('class', this);
}

}
let p = new Person();
p.locate(); // instance, Person {}    构造函数
Person.prototype.locate(); // prototype, {constructor: ... }    类原型
Person.locate(); // class, class Person {}     类静态方法*/


/*class Person {
constructor(age) {
this.age_ = age;
}
sayAge() {
console.log(this.age_);
}
static create() {
// 使用随机年龄创建并返回一个Person 实例
return new Person(Math.floor(Math.random()*100));   //实例工厂   构造函数
}
}
let a = Person.create()   //静态函数
// console.log(a.sayAge()); // Person { age_: ... }
a.sayAge();     //普通函数*/


//class类extends继承
/*class Vehicle { constructor(name){this.name = name;}}
// 继承类
class Bus extends Vehicle {}
let b = new Bus("jack");
console.log(b.name);   //继承name属性
console.log(b instanceof Bus); // true
console.log(b instanceof Vehicle); // true
function Person() {}
// 继承普通构造函数
class Engineer extends Person {}
let e = new Engineer();
console.log(e instanceof Engineer); // true
console.log(e instanceof Person); // true*/


//在派生类中使用super()表示引用父类的构造函数
/*class Vehicle {
constructor() {
this.hasEngine = true;
}
}
class Bus extends Vehicle {
constructor() {
// 不要在调用super()之前引用this，否则会抛出ReferenceError
super(); // 相当于super.constructor()
console.log(this instanceof Vehicle); // true
console.log(this); // Bus { hasEngine: true }
}
}
let bb = new Bus();
console.log(bb.hasEngine);*/



//在派生类中使用super.identify()表示引用父类的静态方法
/*class Vehicle {
static identify() {
console.log('vehicle');
}
}
class Bus extends Vehicle {
static identify() {
super.identify();
}
}
Bus.identify(); // vehicle*/



// 抽象基类，new.target关键字，抽象类只能继承不能实例
/*class Vehicle {
constructor() {
console.log(new.target);
if (new.target === Vehicle) {
throw new Error('Vehicle cannot be directly instantiated');
}
}
}
// 派生类
class Bus extends Vehicle {}
new Bus(); // class Bus {}
new Vehicle(); // class Vehicle {}
// Error: Vehicle cannot be directly instantiated*/



// 抽象基类,派生类必须实现某个方法
/*class Vehicle {
constructor() {
if (new.target === Vehicle) {
throw new Error('Vehicle cannot be directly instantiated');
}
if (!this.foo) {
throw new Error('Inheriting class must define foo()');
}
console.log('success!');
}
}
// 派生类
class Bus extends Vehicle {
foo() {}
}
// 派生类
class Van extends Vehicle {}
new Bus(); // success!
new Van(); // Error: Inheriting class must define foo()*/



/*//多继承
class Vehicle {}
let FooMixin = (Superclass) => class extends Superclass {
foo() {
console.log('foo');
}
};
let BarMixin = (Superclass) => class extends Superclass {
bar() {
console.log('bar');
}
};
let BazMixin = (Superclass) => class extends Superclass {
baz() {
console.log('baz');
}
};
class Bus extends FooMixin(BarMixin(BazMixin(Vehicle))) {}      //通过嵌套模拟实现多继承
let b = new Bus();
b.foo(); // foo
b.bar(); // bar
b.baz(); // baz*/

/*let ints = [1, 2, 3];
console.log(ints.map(function(i) { return i + 1; })); // [2, 3, 4]
console.log(ints.map((i) => { return i + 1 }));

console.log(ints.map((i) => { return i + 1 }));
*/

//Lambda表达式
/*let double = (x) => { return 2 * x; };   //没有参数或多个参数时使用括号
let triple = x => { return 3 * x; };    //一个参数时可省略括号
let triplee = x =>  3 * x;   //大括号表示函数体，不用括号则只能有一条函数语句
console.log(double(3));
console.log(triple(4));
console.log(triplee(5));*/

//Lambda表达式赋值
/*let value = {};
let setName = (x) => {x.name = "Matt"; x.age = 13;}
setName(value);
console.log(value.name); // "Matt"
console.log(value.age); // "Matt"*/

//Enter确认事件,递归函数
/*function ass()
{
    let i = document.getElementById("num").value;
    if(event.keyCode == 13)
    {
        aqq(i);  //闭包
    }
}
function aqq(i)
{
        i = i * 2;
        console.log(i);
        if (i < 100 && i*2 < 100)
        {
            aqq(i);
        }
    }*/

//...拓展操作符
/*let values = [1, 2, 3, 4];
function getSum() {
let sum = 0;
for (let i = 0; i < arguments.length; ++i) {
sum += arguments[i];
}
return sum;
}
// console.log(getSum.apply(null, values)); // 10  数组元素一个一个依次引用
console.log(getSum(...values)); // 10   ...拓展操作符，数组元素依次迭代引用
console.log(getSum(-1,...values)); // 10   ...拓展操作符，数组元素依次迭代引用*/


//callSomeFunction将函数作为参数会传入另一个函数,可把函数看成是一个变量
/*function add10(num) {
return num + 10;
}
let result1 = callSomeFunction(add10, 10);
console.log(result1); // 20
function getGreeting(name) {
return "Hello, " + name;
}
let result2 = callSomeFunction(getGreeting, "Nicholas");
console.log(result2); // "Hello, Nicholas"*/




/*function factorial(num) {   //函数声明，可以代码提升
if (num <= 1) {
return 1;
} else {
return num * arguments.callee(num - 1);    //arguments.callee指代factorial进行递归,即指代调用当前函数
}
}
let trueFactorial = factorial;   //赋值第一个factorial函数
factorial = function() {     //函数表达式，执行时才会编译
return 0;
};
console.log(trueFactorial(5)); // 120
console.log(factorial(5)); // 0*/



//caller属性,指代调用当前函数的函数
/*function outer() {
inner();
}
function inner() {
// console.log(inner.caller);
console.log(arguments.callee.caller);
}
outer();*/

//new.target指代调用构造函数的对象
/*function King() {
if (!new.target) {
throw 'King must be instantiated using "new"'
}
console.log('King instantiated using "new"');
}
new King(); // King instantiated using "new"
King(); // Error: King must be instantiated using "new"

*/


/*function sayName(name) {
console.log(name);
}
function sum(num1, num2) {
return num1 + num2;
}
function sayHi() {
console.log("hi");
}
console.log(sayName.length); // 1    //length表示函数参数的个数，可以模拟函数重载
console.log(sum.length); // 2
console.log(sayHi.length); // 0*/



//apply/call调用函数，类似委托
/*function sum(num1, num2) {
return num1 + num2;
}
function callSum1(num1, num2) {
return sum.apply(this, arguments); // 传入arguments 对象
}
function Sum2(num1, num2) {
return sum.apply(this, [num1, num2]); // 传入数组
}
function callSum3(num1, num2) {
return sum.call(this, num1, num2); // 传入参数
}
console.log(callSum1(10, 10)); // 20
console.log(callSum2(10, 10)); // 20
console.log(Sum3(10, 10)); // 20*/

//this指代调用的上下文
/*window.color = 'red';
let o = {
color: 'blue'
};
function sayColor() {
console.log(this.color);
}
sayColor(); // red
sayColor.call(this); // red
sayColor.call(window); // red
sayColor.call(o); // blue*/



/*window.color = 'red';
var o = {
color: 'blue'
};
function sayColor() {
console.log(this.color);
}
let objectSayColor = sayColor.bind(o);   //bind()方法会创建一个新的函数实例
//sayColor.call(o);
objectSayColor(); // blue*/


//this指代调用函数的对象
/*window.identity = 'The Window';
let object = {
identity: 'My Object',
getIdentityFunc() {
return function() {
return this.identity;      //this表示window
};
}
};
console.log(object.getIdentityFunc()()); // 'The Window'


window.identity = 'The Window';
let object = {
identity: 'My Object',
getIdentityFunc() {
let that = this;       //this表示object
return function() {
return that.identity;       //that表示object
};
}
};
console.log(object.getIdentityFunc()()); // 'My Object'*/



/*function assignHandler() {
let element = document.getElementById('someElement');
element.onclick = () => console.log(element.id);
}*/

// 设置HTML属性值
/*let a = document.getElementById("num");
a.value = "landscape"
console.log(a.value);

$("#num").attr("value",12);

$(function(){
    $("#num").attr("value",12);
})

$(function(){
    $("#num").val(12);
})*/


// HTML标签触发事件
/*let a = document.getElementById("num");
a.onkeydown = function()
{
    if(event.keyCode == 13)
    {
        let b =parseInt(a.value);
        console.log(b + 10);
    }
}

$(function(){
    $("#num").keydown(function(){
        if(event.keyCode == 13)
        {
            let b =parseInt(this.value);
            console.log(b + 10);
        }
    })
})*/


//addEventListener  HTML标签触发事件
/*let divs = document.querySelectorAll('div');
for (var i = 0; i < divs.length; ++i) {
    divs[i].addEventListener('click', (function(frozenCounter) {
        return function() {
            console.log(frozenCounter);
        };
    })(i));
}

let divs = document.querySelectorAll('div');
for (let i = 0; i < divs.length; ++i) {
    divs[i].addEventListener('click', function() {
        console.log(i);
    });
}*/


//多次调用cat函数age变量会累加，当其它方法调用age时会出错，即污染其它程序
/*var age = 18;
function cat(){
    age++;
    console.log(age);// cat函数内输出age，该作用域没有，则向外层寻找，结果找到了，输出[19];
}
cat();//19
cat();
cat();*/
//通过闭包将变量a在方法内外分隔，既能实现累加功能，又不污染其它程序，但使变量不能垃圾回收，造成内存浪费，也容易内存泄漏
/*var a  = 10;
function Add3(){
    var a = 10;
    return function(){
        a++;
        return a;
    };
};
var cc =  Add3();
console.log(cc());
console.log(cc());
console.log(cc());
console.log(a);*/

//querySelector通过选择器选择第一个元素，querySelectorAll返回一个数组
/*let a = document.querySelector("#num");
console.log(a.value);
let b = document.querySelectorAll("#num");
console.log(b[0].value);
console.log(b.length);*/


//setTimeout休眠1秒，1秒后将函数推入任务队列，但不代表立即执行
/*let a = x => console.log(x + 4);
let b = setTimeout(a,1000,3);

clearTimeout(b);   //取消任务*/

// 定时器
// let a = setInterval(() => console.log("Hello world!"), 1000);
//取消定时器
// clearInterval(a)

//深度嵌套的回调函数（俗称“回调地狱”）,早期异步执行操作
/*function double(value) {
setTimeout(console.log, 0, value * 3);
setTimeout(() => setTimeout(console.log, 0, value * 2), 5000);
// setTimeout(() => setTimeout(console.log, 0, value * 2), 5000);
setTimeout(console.log, 2000, value * 4);
}
double(3);*/

//异常处理
/*function double(value, success, failure) {
    setTimeout(() => {
        try {
            if (typeof value !== 'number') {
                throw 'Must provide number as first argument';
            }
            success(2 * value);
        } catch (e) {
            failure(e);
        }
    }, 1000);
}
const successCallback = (x) => {
    double(x, (y) => console.log(`Success: ${y}`));
};
const failureCallback = (e) => console.log(`Failure: ${e}`);
double("3", successCallback, failureCallback);
// Success: 12（大约1000 毫秒之后）*/


//期约
/*let p = new Promise(() => {});
setTimeout(console.log, 0, p); // Promise <pending>

//resolve, reject完成和拒绝，期约只有两个参数
//Promise期约类型
new Promise(() => setTimeout(console.log, 0, 'executor'));
setTimeout(console.log, 0, 'promise initialized');
// executor
// promise initialized
//添加setTimeout 可以推迟切换状态：
let p = new Promise((resolve, reject) => setTimeout(resolve, 1000));
// 在console.log 打印期约实例的时候，还不会执行超时回调（即resolve()）
setTimeout(console.log, 0, p); // Promise <pending>*/

//setTimeout参数reject，resolve改变期约状态
/*let p = new Promise((resolve, reject) => {
setTimeout(reject, 10000); // 10 秒后调用reject()
// 执行函数的逻辑
});
setTimeout(console.log, 0, p); // Promise <pending>
// setTimeout(console.log, 11000, p); // 11 秒后再检查状态
// (After 10 seconds) Uncaught error
// (After 11 seconds) Promise <rejected>*/

//期约初始化为完成状态
/*let p1 = new Promise((resolve, reject) => resolve());
let p2 = Promise.resolve();*/


//期约拒绝
/*let p = Promise.reject(3);
setTimeout(console.log, 0, p); // Promise <rejected>: 3
// 期约拒绝错误处理程序,then两个参数：nResolved、onRejected,不传参数则用null表示
p.then((e) => setTimeout(null, (e) => setTimeout(console.log, 1000, e)); // 3*/


//catch方法相当于p.then(null, onRejected);
/*let p = Promise.reject();
let onRejected = function(e) {
setTimeout(console.log, 0, 'rejected');
};
// 这两种添加拒绝处理程序的方式是一样的：
p.then(null, onRejected); // rejected
p.catch(onRejected); // rejected

Promise.prototype.catch()   //返回一个新的期约实例：
let p1 = new Promise(() => {});
let p2 = p1.catch();
setTimeout(console.log, 0, p1); // Promise <pending>
setTimeout(console.log, 0, p2); // Promise <pending>
setTimeout(console.log, 0, p1 === p2); // false*/



//then为期约的后续处理程序，实现thenable接口，在期约状态改变后运行，需要实现onResolved和onRejected两个方法
/*function onResolved(id) {
setTimeout(console.log, 0, id, 'resolved');
}
function onRejected(id) {
setTimeout(console.log, 0, id, 'rejected');
}
let p1 = new Promise((resolve, reject) => setTimeout(resolve, 3000));
let p2 = new Promise((resolve, reject) => setTimeout(reject, 3000));
//期约的后续处理程序
p1.then(() => onResolved('p1'),
() => onRejected('p1'));
p2.then(() => onResolved('p2'),
() => onRejected('p2'));
//（3 秒后）
// p1 resolved
// p2 rejected*/


//期约拒绝错误不能被try捕获
/*try {
throw new Error('foo');
} catch(e) {
console.log(e); // Error: foo
}
try {
Promise.reject(new Error('bar'));
} catch(e) {
console.log(e);
}
// Uncaught (in promise) Error: bar*/

//期约状态改变后运行，无论是完成还是拒绝最后都会执行此代码
/*let p1 = Promise.resolve();
let p2 = Promise.reject();
let onFinally = function() {
setTimeout(console.log, 0, 'Finally!')
}
p1.finally(onFinally); // Finally
p2.finally(onFinally); // Finally
*/

//非重入性，先执行同步代码，再执行then()处理程序，包括resolve,reject,cath,finally
/*// 创建解决的期约
let p = Promise.resolve();
// 添加解决处理程序
// 直觉上，这个处理程序会等期约一解决就执行
p.then(() => console.log('onResolved handler'),null);
// 同步输出，证明then()已经返回
console.log('then() returns');
// 实际的输出：
// then() returns
// onResolved handler
*/

//期约拒绝的错误理由
/*let p1 = new Promise((resolve, reject) => reject(Error('foo')));
let p2 = new Promise((resolve, reject) => { throw Error('foo'); });
let p3 = Promise.resolve().then(() => { throw Error('foo'); });
let p4 = Promise.reject(Error('foo'));
setTimeout(console.log, 0, p1); // Promise <rejected>: Error: foo
setTimeout(console.log, 0, p2); // Promise <rejected>: Error: foo
setTimeout(console.log, 0, p3); // Promise <rejected>: Error: foo
setTimeout(console.log, 0, p4); // Promise <rejected>: Error: foo*/



//BOM window窗口大小
/*$(function(){
    $(".but").click(function(){
        //浏览器网页视口大小
        console.log(`宽度：${document.body.clientWidth} 高度：${document.body.clientHeight}`);
        //浏览器自身窗口大小
        console.log(`宽度：${window.innerWidth} 高度：${window.innerWidth}`);
    })
})
let pageWidth = window.innerWidth,
pageHeight = window.innerHeight;
if (typeof pageWidth != "number") {
if (document.compatMode == "CSS1Compat"){
pageWidth = document.documentElement.clientWidth;
pageHeight = document.documentElement.clientHeight;
} else {
pageWidth = document.body.clientWidth;
pageHeight = document.body.clientHeight;
}
}
console.log(`宽度：${pageWidth} 高度：${pageHeight}`)*/

//window.open打开一个新的窗口,相当于a标签,第二个参数可以为_self、_parent、_top、_blank或其它自定义名,第三个参数表示新窗口的特性
//a.opener表示新窗口的顶层窗口，即window
/*$(function(){
    $(".but").click(function(){
        let a = window.open("./tabletest.html", "_blank","height=400,width=400,top=10,left=10,resizable=yes");
        console.log(a.opener==window);
        a.opener = null;   //表示新窗口可以独立运行在一个进程中，与顶层窗口断开连接
    })
})*/

//一加载就直接弹窗会被拦截，窗口返回null
/*let wroxWin = window.open("./tabletest.html", "_blank");
if (wroxWin == null){
    alert("The popup was blocked!");
}
*/

//某些浏览器拦截弹出式窗口会引发错误，用catch捕获错误进行异常处理
/*let blocked = false;
try {
    let wroxWin = window.open("http://www.wrox.com", "_blank");
    if (wroxWin == null){
        blocked = true;
    }
} catch (ex){
    blocked = true;
}
if (blocked){
    alert("The popup was blocked!");
}*/


// 系统确认框
/*let b =confirm("fjog");
if(b)
{
    console.log("OK");
}
else
{
    console.log("NG");
}
*/

//获取get请求问题之后的参数
/*let getQueryStringArgs = function() {
// 取得没有开头问号的查询字符串
/*let qs = (location.search.length > 0 ? location.search.substring(1) : ""),
// 保存数据的对象
args = {};
// 把每个参数添加到args 对象
for (let item of qs.split("&").map(kv => kv.split("="))) {
    let name = decodeURIComponent(item[0]),
    value = decodeURIComponent(item[1]);     //URL解码
    if (name.length) {
        args[name] = value;
    }
}
return args;
}*/



// URLSearchParams接口，用于检查和修改GET查询字符串
/*let qs = "?q=javascript&num=10";
let searchParams = new URLSearchParams(qs);    //实例一个GET查询参数
alert(searchParams.toString()); // " q=javascript&num=10"
searchParams.has("num"); // true
searchParams.get("num"); // 10
searchParams.set("page", "3");
alert(searchParams.toString()); // " q=javascript&num=10&page=3"
searchParams.delete("q");
alert(searchParams.toString()); // " num=10&page=3"*/


// location.assign("https://cn.bing.com/");
// window.location = "https://cn.bing.com/";
// location.href = "https://cn.bing.com/";


//三种方式修改网页地址
/*$(function(){
    $(".but").click(function(){
        // location.assign("https://cn.bing.com/");       //修改网页地址,浏览记录会增加一笔
        window.location = "https://cn.bing.com/";    //修改网页地址,浏览记录会增加一笔
        // location.href = "https://cn.bing.com/";      //修改网页地址,浏览记录会增加一笔
        location.replace("https://cn.bing.com/");    //修改网页地址,浏览记录不会增加一笔
    })
})*/


//刷新页面
/*$(function(){
    $(".but").click(function(){
        location.reload();      //可能从缓存中重新加载页面，即刷新页面
        location.reload(true);   //从服务器中重新加载页面
    })
})
*/


//navigator对象属性
/*$(function(){
    $(".but").click(function(){
        // console.log(navigator.vendor);
        console.log(window.navigator.plugins);
    })
})*/


//navigator.plugins检查浏览器是否安装某个插件
/*let hasPlugin = function(name) {
name = name.toLowerCase();
for (let plugin of window.navigator.plugins){
if (plugin.name.toLowerCase().indexOf(name) > -1){
return true;
}
}
return false;
}
// 检测Chrome PDF Viewer
alert(hasPlugin("Chrome PDF Viewer"));
// 检测QuickTime
alert(hasPlugin("QuickTime"));*/


//registerProtocolHandler将网站注册为处理程序，如在线打开处理PDF，第一个参数表示要处理的协议，第二个参数表示WEB应用程序地址，第三个参数表示WEB应用名称
/*navigator.registerProtocolHandler("mailto",
"http://www.somemailclient.com?cmd=%s",
"Some Mail Client");*/


//检测浏览器是否支持某个功能
/*if (object.propertyInQuestion) {
// 使用object.propertyInQuestion
}
// 比如，IE5 之前的版本中没有document.getElementById()这个DOM 方法，但可以通过
// document.all 属性实现同样的功能。为此，可以进行如下能力检测：
//检测浏览器是否支持document.getElementById
function getElement(id) {
    if (document.getElementById) {
        return document.getElementById(id);
    } else if (document.all) {
        return document.all[id];
    } else {
        throw new Error("No way to retrieve element!");
    }
}*/


/*function isSortable(object) {
return typeof object.sort == "function";
}

let hasNSPlugins = !!(navigator.plugins && navigator.plugins.length);
// 检测浏览器是否具有DOM Level 1 能力
let hasDOM1 = !!(document.getElementById && document.createElement &&
document.getElementsByTagName);*/


/*$(function(){
    $(".but").click(function(){
        // console.log(navigator.vendor);
        console.log(document.getElementsByClassName);
    })
})
*/

//查询，类LINQ
/*let b = document.querySelector(".but")
console.log(b.value);*/


//浏览器能力检测
/*/*function checkfun(object,funName)
{
    if(funName)
    {
        console.log(typeof object[funName]);
        if( typeof object[funName] == "function")
        {
            console.log("ok");
        }
        else
        {
            console.log("NG");
        }
    }
}
checkfun(document,"getElementsByClassName");



function isHostMethid(obj,pro)
        {
            var t=typeof(obj[pro]);
            alert(t);
            return t=='function'||  //t可能是一个函数
            (!!(t=='object'&&object[pro]))||    
            t=='unkmow';    //用typeof检测ActiveX对象v
        }
        isHostMethid(window,"alert")*/



//怪癖检测
//IE8及更早的版本存在的bug，如果某个示例属性与标记为[[DontEnum]]属性名相同，那么该实例属性不会出现在for-in循环中
/*var hasDontEnumQuirk=function(){
    var o={toString:function(){}};
    for(var prop in o){
        if(prop=='toString'){
            return false;
        }
    }
    return true;
}
console.log(hasDontEnumQuirk());*/




//用户代理检测查询使用的浏览器
/*var client = function () {

    // rendering engines
    var engine = {
        ie: 0,
        gecko: 0,
        webkit: 0,
        khtml: 0,
        opera: 0,

        // complete version
        ver: null
    };

    // browsers
    var browser = {

        // browsers
        ie: 0,
        firefox: 0,
        safari: 0,
        konq: 0,
        opera: 0,
        chrome: 0,

        // specific version
        ver: null
    };

    // platform/device/OS
    var system = {
        win: false,
        mac: false,
        x11: false,

        // mobile devices
        iphone: false,
        ipod: false,
        ipad: false,
        ios: false,
        android: false,
        nokiaN: false,
        winMobile: false,

        // game systems
        wii: false,
        ps: false
    };
    // wx/qqZone/   
    var _client = {
        wx: false,
        qqZone: false
    }

    // detect rendering engines/browsers
    var ua = navigator.userAgent;
    _client.wx = ua.match(/MicroMessenger/i) == "micromessenger";
    _client.qqZone = ua.match(/QQ/i) == "qq";
    if (window.opera) {
        engine.ver = browser.ver = window.opera.version();
        engine.opera = browser.opera = parseFloat(engine.ver);
    } else if (/AppleWebKit\/(\S+)/.test(ua)) {
        engine.ver = RegExp["$1"];
        engine.webkit = parseFloat(engine.ver);

        // figure out if it's Chrome or Safari
        if (/Chrome\/(\S+)/.test(ua)) {
            browser.ver = RegExp["$1"];
            browser.chrome = parseFloat(browser.ver);
        } else if (/Version\/(\S+)/.test(ua)) {
            browser.ver = RegExp["$1"];
            browser.safari = parseFloat(browser.ver);
        } else {
            // approximate version
            var safariVersion = 1;
            if (engine.webkit < 100) {
                safariVersion = 1;
            } else if (engine.webkit < 312) {
                safariVersion = 1.2;
            } else if (engine.webkit < 412) {
                safariVersion = 1.3;
            } else {
                safariVersion = 2;
            }

            browser.safari = browser.ver = safariVersion;
        }
    } else if (/KHTML\/(\S+)/.test(ua) || /Konqueror\/([^;]+)/.test(ua)) {
        engine.ver = browser.ver = RegExp["$1"];
        engine.khtml = browser.konq = parseFloat(engine.ver);
    } else if (/rv:([^\)]+)\) Gecko\/\d{8}/.test(ua)) {
        engine.ver = RegExp["$1"];
        engine.gecko = parseFloat(engine.ver);

        // determine if it's Firefox
        if (/Firefox\/(\S+)/.test(ua)) {
            browser.ver = RegExp["$1"];
            browser.firefox = parseFloat(browser.ver);
        }
    } else if (/MSIE ([^;]+)/.test(ua)) {
        engine.ver = browser.ver = RegExp["$1"];
        engine.ie = browser.ie = parseFloat(engine.ver);
    }

    // detect browsers
    browser.ie = engine.ie;
    browser.opera = engine.opera;

    // detect platform
    var p = navigator.platform;
    system.win = p.indexOf("Win") == 0;
    system.mac = p.indexOf("Mac") == 0;
    system.x11 = (p == "X11") || (p.indexOf("Linux") == 0);

    // detect windows operating systems
    if (system.win) {
        if (/Win(?:dows )?([^do]{2})\s?(\d+\.\d+)?/.test(ua)) {
            if (RegExp["$1"] == "NT") {
                switch (RegExp["$2"]) {
                    case "5.0":
                        system.win = "2000";
                        break;
                    case "5.1":
                        system.win = "XP";
                        break;
                    case "6.0":
                        system.win = "Vista";
                        break;
                    case "6.1":
                        system.win = "7";
                        break;
                    default:
                        system.win = "NT";
                        break;
                }
            } else if (RegExp["$1"] == "9x") {
                system.win = "ME";
            } else {
                system.win = RegExp["$1"];
            }
        }
    }

    // mobile devices
    system.iphone = ua.indexOf("iPhone") > -1;
    system.ipod = ua.indexOf("iPod") > -1;
    system.ipad = ua.indexOf("iPad") > -1;
    system.nokiaN = ua.indexOf("NokiaN") > -1;

    // windows mobile
    if (system.win == "CE") {
        system.winMobile = system.win;
    } else if (system.win == "Ph") {
        if (/Windows Phone OS (\d+.\d+)/.test(ua)) {
            ;
            system.win = "Phone";
            system.winMobile = parseFloat(RegExp["$1"]);
        }
    }

    // determine iOS version
    if (system.mac && ua.indexOf("Mobile") > -1) {
        if (/CPU (?:iPhone )?OS (\d+_\d+)/.test(ua)) {
            system.ios = parseFloat(RegExp.$1.replace("_", "."));
        } else {
            system.ios = 2;  // can't really detect - so guess
        }
    }

    // determine Android version
    if (/Android (\d+\.\d+)/.test(ua)) {
        system.android = parseFloat(RegExp.$1);
    }

    // gaming systems
    system.wii = ua.indexOf("Wii") > -1;
    system.ps = /playstation/i.test(ua);

    // return it
    return {
        engine: engine,
        browser: browser,
        system: system,
        _client: _client
    }
}();*/

//检测节点类型
/*let someNode = document.body;
if (someNode.nodeType == Node.ELEMENT_NODE){
    alert("Node is an element.");
}*/

// 读取修改文档标题
/*let originalTitle = document.title;
console.log(originalTitle);
document.title = "New page title";
let qq = document.title;
console.log(qq);
let aa = document.referrer;
console.log(qq);*/

//获取标签名
/*let div = document.getElementById("myDiv");
alert(div.tagName); // "DIV"
alert(div.nodeName); // "DIV"*/





/*$(function(){
    $(".but").click(function(){
        let div = document.getElementById("ppp");
        div.className="div2";
    })
})*/



//遍历标签的元素属性
/*function outputAttributes(element) {
    let pairs = [];
    for (let i = 0, len = element.attributes.length; i < len; ++i) {
        const attribute = element.attributes[i];
        pairs.push(`${attribute.nodeName}="${attribute.nodeValue}"`);
    }
    return pairs.join(" ");
}
let a = document.getElementsByClassName("but");
let b = outputAttributes(a[0]);
console.log(b);*/


//修改TEXT节点
/*$(function(){
    $(".but").click(function(){
        let a = document.getElementById("p1");
        let texta = a.firstChild;
        texta.nodeValue = "修改";
    })
})*/


//新增Text节点
/*$(function(){
    $(".but").click(function(){
        let a = document.getElementById("p1");
let textNode = document.createTextNode("Hello world!");　　　//新建Text节点
a.appendChild(textNode);     //一个P元素中含有两个文本节点
a.normalize();     //两个文本节点合并为一个文本节点
let newNode = a.firstChild.splitText(5);     //一个文本节点拆分为两个文本节点，返回的节点包含第5位之后的字符
})
})*/



//一个对象上绑定多个事件
/*$(function(){
    $(".but").bind({
        "dblclick":function()
        {
            alert("hello");
        },
        "mouseout":function()
        {
            alert(document.doctype.name);      //doctype类型
        }
             
    })
})*/


/*$(function(){
    $(".but").on({
        "dblclick":function()
        {
            alert("hello");
        },
        "mouseout":function()
        {
            alert(document.doctype.name);      //doctype类型
        }

    })
})
*/


//新建文档片段并一次性增加到文档中，以避免分次刷新页面的性能消耗
/*let fragment = document.createDocumentFragment();
let ul = document.getElementById("myList");
for (let i = 0; i < 3; ++i) {
    let li = document.createElement("li");
    li.appendChild(document.createTextNode(`Item ${i + 1}`));
    fragment.appendChild(li);
}
ul.appendChild(fragment);*/


// 属性操作
/*let attr = document.createAttribute("align");    //创建属性
attr.value = "left";
element.setAttributeNode(attr);     //属性添加到元素
alert(element.attributes["align"].value); // "left"
alert(element.getAttributeNode("align").value); // "left"   //获取属性
alert(element.getAttribute("align"));*/


// 动态脚本
/*let script = document.createElement("script");
script.appendChild(document.createTextNode("function sayHi(){alert('hi');}"));
document.body.appendChild(script);


var script = document.createElement("script");
script.text = "function sayHi(){alert('hi');}";
document.body.appendChild(script);


var script = document.createElement("script");
var code = "function sayHi(){alert('hi');}";
try {
script.appendChild(document.createTextNode("code"));
} catch (ex){
script.text = "code";
}
document.body.appendChild(script);


function loadScriptString(code){
var script = document.createElement("script");
script.type = "text/javascript";
try {
script.appendChild(document.createTextNode(code));
} catch (ex){
script.text = code;
}
document.body.appendChild(script);
loadScriptString("function sayHi(){alert('hi');}");
}
*/


/*function printage(a)
{
    alert(a);
}
function movebyc()
{
    window.open("http://www.baidu.com","_blank");
}*/


/*$(function(){
    $(".button2").click(function(){
        window.open("http://www.baidu.com","_blank");
    })
})*/


//手写解析GET查询字符串
/*let getQueryStringArgs = function() {
    let serchaa = "https://cn.bing.com/search?q=%E9%97%AE%E9%A2%98&qs=n&form=QBRE&sp=-1&lq=0&pq=%E9%97%AE%E9%A2%98&sc=10-2&sk=&cvid=8F2B4B727A864DAB969138C69BEC682E&ghsh=0&ghacc=0&ghpl=";
// 取得没有开头问号的查询字符串
let qs = (serchaa.length > 0 ? serchaa.substring(1) : ""),
// 保存数据的对象
args = {};
// 把每个参数添加到args 对象
for (let item of qs.split("&").map(kv => kv.split("="))) {
let name = decodeURIComponent(item[0]),
value = decodeURIComponent(item[1]);   //URL解码
if (name.length) {
args[name] = value;
}
}
return args;
}*/


// window事件
/*window.addEventListener("beforeunload", (event) => {
    event.returnValue ="dgrgr";
});*/
/*window.addEventListener("beforeunload", (event) => {
    let message = "dgrgr";
    event.returnValue =message;
    return message;
});*/


/*$(function(){
    $("#mydiv").contextmenu(function(event){      //contextmenu右键快捷方式事件
        event.preventDefault();

        let menu = document.getElementById("ull");
        menu.style.left = event.clientX + "px";
        menu.style.top = event.clientY + "px";
        menu.style.visibility = "visible";    //初始为不可见，右键后改为可见状态
    })

})
document.addEventListener("click", (event) => {
    document.getElementById("ull").style.visibility = "hidden";});*/



/*$(function(){
    $("#ul1").click(function(event){    //事件委托，减少事件占用内存
        let e = event.target;     //獲取觸發事件和對象
        switch(e.id)
        {
            case "li1": console.log("li1");break;
            case "li2": console.log("li2");break;
            case "li3": console.log("li3");break;
        }
    })
})*/



// $(function(){
    // $(".but").click(function(){
        //window.open("http://www.baidu.com","newnew","resizable=no");
        /*let a = prompt();
        document.write(a);*/
        /*let a = confirm("yes or no");
        document.write(a);*/

      /*let aa=  getQueryStringArgs();
      document.write(aa["pq"])*/

      // setTimeout(() => location.href="http://www.baidu.com",2000);
      
      /*for(let plu of window.navigator.plugins)
      {
        console.log(plu.name.toLowerCase());
    }*/
    //navigator.getBattery().then((b) => console.log(b));  //检查设备是否在充电

     /*let element = document.getElementById("radio1");
    let a = element.attributes.getNamedItem("name").nodeValue;   //获取属性值
    console.log(element.name);
    console.log(a);
    element.attributes.removeNamedItem("name"); */ //删除属性


    /*var MutationObserver = window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver
        const container = document.getElementById('fo1')
        const options = {
            childList: true, // 表示当前元素的childNodes发生变化的时候才会触发回调函数
        }
        // 创建MutationObserver实例，返回一个观察者对象
        const mutation = new MutationObserver(function(mutationRecoards, observer) {
            console.log(mutationRecoards)
            console.log(observer)
        })
        // 对观察者添加需要观察的元素，并设置需要观察元素的哪些方面
        mutation.observe(container, options);
        */


    /*const nod = document.getElementById("fo1");
    const op = {childList: true,     //监视childList的变化
                subtree: true,       //监视子代及孙代的变化，没有这名只监视子代的变化
                //attributes: true,    //监视对象属性的变化
                //attributeFilter: ['name']    //监视指定属性的变化
                //characterData:true    //监视对象文本内容的变化
                };   
    //mutationRecoards记录变化的信息
    let observer = new MutationObserver((mutationRecoards, observer) => {console.log('<body> attributes changed')} );    //属性有变化则执行回调函数
    observer.observe(nod,op);        //监视fo1的childList属性的变化
    //observer.disconnect()   //停止监视*/


    /*let ele = document.createElement("input");
    let br = document.createElement("br");
    ele.type = "radio";
    ele.name = "rad";
    ele.id = "radio3";
    let parEle = document.getElementById("fo1");
    parEle.appendChild(br);
    parEle.appendChild(ele);
    let txt = document.createTextNode(" Hello world");
    parEle.appendChild(txt);         //动态添加标签

    let getele = document.getElementById("radio3");
    let att = getele.attributes["name"].value;
    console.log(att);*/



    /*let table = document.createElement("table");
    table.border = 1;
    table.width = "60%";
    // 创建表体
    let tbody = document.createElement("tbody");
    table.appendChild(tbody);
    // 创建第一行
    tbody.insertRow(0);
    tbody.rows[0].insertCell(0);
    tbody.rows[0].cells[0].appendChild(document.createTextNode("Cell 1,1"));
    tbody.rows[0].insertCell(1);
    tbody.rows[0].cells[1].appendChild(document.createTextNode("Cell 2,1"));
    // 创建第二行
    tbody.insertRow(1);
    tbody.rows[1].insertCell(0);
    tbody.rows[1].cells[0].appendChild(document.createTextNode("Cell 1,2"));
    tbody.rows[1].insertCell(1);
    tbody.rows[1].cells[1].appendChild(document.createTextNode("Cell 2,2"));
    // 把表格添加到文档主体
    document.body.appendChild(table);
    */

    /*let a = document.querySelector("#fo1").classList;   //获取对象类名列表
    console.log(a);
    a.remove("user");
    a.add("first");
    a.toggle("second");   //有则删除，无则添加
    console.log(a.contains("first"));
    console.log(a);
    */

//console.log(document.activeElement.nodeType);  //当前活动的元素

//console.log(document.hasFocus)   //判断文档是否获得焦点，可知道用户是否在操作页面

//console.log(document.readyState);    //loading表示文档正在加载，complet表示文档加载完成
/*0: 请求未初始化
1: 服务器连接已建立
2: 请求已接收
3: 请求处理中
4: 请求已完成，且响应已就绪*/

/*let ele = document.getElementById("radio2").dataset["aaa"];   //data-aaa="userid"自定义属性
console.log(ele);*/

// document.getElementsByTagName("p").innerHTML="hello world";

/*let pp = document.getElementsByTagName("p")[0];
pp.innerText = "ppppppp"
pp.innerHTML +="<i>hello world</i>";
setTimeout(()=> pp.outerHTML="<i>qqqqqq</i>",2000);*/


//获取文本框被选中的字符
/*let txt = document.getElementById("txtbox");
let t = txt.value.substring(txt.selectionStart,txt.selectionEnd);
console.log(t + "+message+");*/

/*let txtbox = document.getElementById("txtbox");
txtbox.addEventListener("keypress", (event) => {
    event.preventDefault();
    console.log("a");
});
*/


//檢查HTML表單數據是否符合元素約束條件
/*if (document.forms[0].elements[1].checkValidity()){
 alert("Y");
} else {
alert("N");
}*/


//customError：如果设置了setCustomValidity()就返回true，否则返回false。
//patternMismatch：如果字段值不匹配指定的pattern 属性则返回true。
//rangeOverflow：如果字段值大于max 的值则返回true。
//rangeUnderflow：如果字段值小于min 的值则返回true。
//stepMisMatch：如果字段值与min、max 和step 的值不相符则返回true。
//tooLong：如果字段值的长度超过了maxlength 属性指定的值则返回true。某些浏览器，如Firefox 4 会自动限制字符数量，因此这个属性值始终为false。
//typeMismatch：如果字段值不是"email"或"url"要求的格式则返回true。
//valid：如果其他所有属性的值都为false 则返回true。与checkValidity()的条件一致。
//valueMissing：如果字段是必填的但没有值则返回true。
/*let input = document.forms[0].elements[2];
if (input.validity && !input.validity.valid){
if (input.validity.valueMissing){       //檢測必填項
console.log("Please specify a value.")
} else if (input.validity.typeMismatch){     //檢測email格式
console.log("Please enter an email address.");
} else {
console.log("Value is invalid.");
}
}*/

// let se = document.getElementById("sele");
// let se = document.forms[0].elements["opti"];
// let newOption = new Option("Option text","Option value");
// se.add(newOption,undefined);
// se.appendChild(newOption);
// se.removeChild(se.options[2]);
// })
// })


//终止事件，禁止输入
/*let txtbox = document.getElementById("txtbox")
txtbox.addEventListener("keypress", (event) => {
event.preventDefault();   
});*/
/*$(function(){
    let txtbox = document.getElementById("txtbox")
txtbox.addEventListener("keypress", (event) => {
    console.log("a");
event.preventDefault();
});
})*/
/*$(function(){
    let txtbox = document.getElementById("txtbox")
txtbox.addEventListener("keypress", (event) => {
txtbox.value = null;
});
})*/


//利用正则过滤文本框只允许输入数量
/*let textbox = document.getElementById("txtbox")
textbox.addEventListener("keypress", (event) => {
if (!/\d/.test(String.fromCharCode(event.charCode)) && event.charCode > 9){
event.preventDefault();
}
});
*/


//粘貼板事件，文本框只允許粘貼數字
/*let textbox = document.getElementById("txtbox");
textbox.addEventListener("paste", (event) => {    //paste在粘貼時觸發  beforecopy在複製前觸發   copy在複製時觸發   beforecut在剪切前觸發   cut在疧時觸發    beforepaste在粘貼前觸發
let text = getClipboardText(event);   //獲得粘貼板的內容
if (!/^\d*$/.test(text)){
event.preventDefault();
}
})
//
function getClipboardText(event){
var clipboardData = (event.clipboardData || window.clipboardData);    //clipboardData粘貼板對象
return clipboardData.getData("text");   //獲得粘貼板的內容
}

function setClipboardText (event, value){
if (event.clipboardData){
return event.clipboardData.setData("text/plain", value);
} else if (window.clipboardData){
return window.clipboardData.setData("text", value);
}
}*/

/*navigator.getBattery().then((battery) => {   //监视电池使用情况
// 添加充电状态变化时的处理程序
const chargingChangeHandler = function(){    //定义一个方法
    if(battery.charging)
    {
        console.log("在充电");
    }
    else
    {
        console.log("已断电");
    }
}
battery.onchargingchange = chargingChangeHandler;    //将方法注册到onchargingchange事件，在充电状态改变时触发
})*/



//表單序列化
/*function serialize(form) {
    let parts = [];
    let optValue;
    for (let field of form.elements) {
        switch(field.type) {
            case "select-one":
            case "select-multiple":
            if (field.name.length) {
                for (let option of field.options) {
                    if (option.selected) {
                        if (option.hasAttribute){
                            optValue = (option.hasAttribute("value") ? option.value : option.text);
                        } else {
                            optValue = (option.attributes["value"].specified ? option.value : option.text);
                        }
                        parts.push(encodeURIComponent(field.name)} + "=" + encodeURIComponent(optValue);        //URL编码
                    }
                }
            }
        break;
case undefined: // 字段集
case "file": // 文件输入
case "submit": // 提交按钮
case "reset": // 重置按钮
case "button": // 自定义按钮
break;
case "radio": // 单选按钮
case "checkbox": // 复选框
if (!field.checked) {
    break;
}
default:
// 不包含没有名字的表单字段
if (field.name.length) {
    parts.push('${encodeURIComponent(field.name)}=' + '${encodeURIComponent(field.value)}');
}
}
return parts.join("&");
}

$(function(){
    $("#sub").click(function(){
        let fo = document.getElementById("fo1");
        let url = serialize(fo);
        console.log(url)

    })
})*/



// window.addEventListener("load", () => {
// frames["ifr"].document.designMode = "on";
// });


//通过表单提交富文本
/*form.addEventListener("submit", (event) => {
let target = event.target;
target.elements["comments"].value =
document.getElementById("richedit").innerHTML;
});*/



/*const textEncoder = new TextEncoder();
const decodedText = 'foo';
const a = textEncoder.encode(decodedText);    //转换为二进制

const dd = "dfgh";
const aa = new Uint8Array(5);
const tt = textEncoder.encodeInto(dd,aa);
console.log(a[1]);
console.log(aa[0]);*/


//文件表单
/*let fi = document.getElementById("fi");
fi.addEventListener("change",(event)=>{
    let file = event.target.files[0];
        console.log(file.name);    //size文件大小，lastModifiedDate文件最后修改时间
    })*/



// 网页读取本地文件
/*    let filesList = document.getElementById("fi");
    filesList.addEventListener("change", (event) => {
        let info = "",
        output = document.getElementById("output"),
        progress = document.getElementById("progress"),
        files = event.target.files,
        type = "default",
        reader = new FileReader();    //FileReader API异步读取文件
        if (/image/.test(files[0].type)) {     //正则匹配
            reader.readAsDataURL(files[0]);        //如果是图片则获取图片的路径
            type = "image";
        } else {
            reader.readAsText(files[0],"utf-8");       //如果是文本则读取文件字符
            type = "text";
        }
        reader.abort();      //终止文件读取
        reader.onerror = function() {
            output.innerHTML = "Could not read file, error code is " +
            reader.error.code;
        };
        reader.onprogress = function(event) {
            if (event.lengthComputable) {
                progress.innerHTML = `${event.loaded}/${event.total}`;
            }
        };
        reader.onload = function() {
            let html = "";
            switch(type) {
                case "image":
                html = `<img src="${reader.result}">`;       //在网页中加载图片
                break;
                case "text":
                html = reader.result;        //如果是文件文件，文件内容存放在result属性中
                break;
            }
            output.innerHTML = html;        //在网页中显示文本文件的内容
        };

        
    });*/



/*let filesList = document.getElementById("fi");
filesList.addEventListener("change", (event) => {
let info = "",
output = document.getElementById("output"),
progress = document.getElementById("progress"),
files = event.target.files,
reader = new FileReader(),
url = window.URL.createObjectURL(files[0]);   //通过文件直接生成URL在内存中
if (url) {
if (/image/.test(files[0].type)) {
output.innerHTML = `<img src="${url}">`; //通过内存中的地址生成图片
} else {
output.innerHTML = "Not an image.";
}
} else {
output.innerHTML = "Your browser doesn't support object URLs.";
}

// window.URL.revokeObjectURL(url)   //释放内存中的URL
});
*/

// Blob对象
/*let myBlobParts = ['<html><h2>Hello Semlinker</h2></html>']; // an array consisting of a single DOMString
let myBlob = new Blob(myBlobParts, {type : 'text/html', endings: "transparent"}); // the blob
console.log(myBlob.size + " bytes size");
console.log(myBlob.type + " is the type");*/



// 读取拖放文件
/*let droptarget = document.getElementById("droptarget");
function handleEvent(event) {
let info = "",
output = document.getElementById("output"),
files, i, len;
event.preventDefault();
if (event.type == "drop") {
files = event.dataTransfer.files;
i = 0;
len = files.length;
while (i < len) {
info += `${files[i].name} (${files[i].type}, ${files[i].size} bytes)<br>`;
i++;
}
output.innerHTML = info;
}
}
droptarget.addEventListener("dragenter", handleEvent);
droptarget.addEventListener("dragover", handleEvent);
droptarget.addEventListener("drop", handleEvent);
*/


//自定义多媒体播放器
// 取得元素的引用
/*let player = document.getElementById("player"),
btn = document.getElementById("video-btn"),
curtime = document.getElementById("curtime"),
duration = document.getElementById("duration");
// 更新时长
duration.innerHTML = player.duration;
// 为按钮添加事件处理程序
btn.addEventListener( "click", (event) => {
if (player.paused) {
player.play();
btn.value = "Pause";
} else {
player.pause();
btn.value = "Play";
}
});
// 周期性更新当前时间
setInterval(() => {
curtime.innerHTML = player.currentTime;
}, 250);*/



//拖放事件
/*dragstart    //被拖放元素选中时触发
drag      //被拖放元素移动时触发
dragend    //被拖放元素停止拖放时触发
dragenter   //在拖放元素被拖入目标元素时目标元素触发
dragover  //在拖放元素被拖入目标元素并继续拖放时目标元素触发
dragleave或drop    //在拖放元素被拖入目标元素无效时触发dragleave，有效时触发drop*/
// 传递文本

// 拖放事件中传递文本或URL
/*event.dataTransfer.setData("text", "some text");
let text = event.dataTransfer.getData("text");
// 传递URL
event.dataTransfer.setData("URL", "http://www.wrox.com/");
let url = event.dataTransfer.getData("URL");*/


/*Notification.requestPermission()
.then((permission) => {
console.log('User responded to permission request:', permission);
});


/*Notification.requestPermission()

document.visibilityState     //hidden表示页面被隐藏    visible表示页面可见     prerender表示页面被隐藏但预览可见
document.visibilitychange     //页面可见性改变时触发
*/



// 数据流，可读流
/*async function* ints() {
// 每1000 毫秒生成一个递增的整数
for (let i = 0; i < 5; ++i) {
yield await new Promise((resolve) => setTimeout(resolve, 1000, i));
}
}

const readableStream = new ReadableStream({
async start(controller) {
for await (let chunk of ints()) {
controller.enqueue(chunk);        //把值写入控制器
}
controller.close();  //关闭控制器
}
});

console.log(readableStream.locked); // false
const readableStreamDefaultReader = readableStream.getReader();    //实例一个读取器
console.log(readableStream.locked); // true        //获取读取器的锁，表示只有这个读取器可以读取数据
// 消费者
(async function() {        //匿名方法
while(true) {
const { done, value } = await readableStreamDefaultReader.read();    //读取数据
if (done) {
break;
} else {
console.log(value);
}
}
})();*/


//可写流
/*async function* ints() {     //生成器
// 每1000 毫秒生成一个递增的整数
for (let i = 0; i < 5; ++i) {
yield await new Promise((resolve) => setTimeout(resolve, 1000, i));
}
}

const writableStream = new WritableStream({write(value) {   //实例一个写入流
console.log(value);
}
});

console.log(writableStream.locked); // false
const writableStreamDefaultWriter = writableStream.getWriter();    //获取写入流的锁
console.log(writableStream.locked); // true
// 生产者
(async function() {
for await (let chunk of ints()) {
await writableStreamDefaultWriter.ready;        //写入流必须为可读
writableStreamDefaultWriter.write(chunk);       //数据写入流
}
writableStreamDefaultWriter.close();          //关闭写入流
})();*/



//转换流
/*async function* ints() {
// 每1000 毫秒生成一个递增的整数
for (let i = 0; i < 5; ++i) {
yield await new Promise((resolve) => setTimeout(resolve, 1000, i));
}
}
const { writable, readable } = new TransformStream({
transform(chunk, controller) {
controller.enqueue(chunk * 2);
}
});
const readableStreamDefaultReader = readable.getReader();
const writableStreamDefaultWriter = writable.getWriter();
// 消费者
(async function() {
while (true) {
const { done, value } = await readableStreamDefaultReader.read();
if (done) {
break;
} else {
console.log(value);
}
}
})();
// 生产者
(async function() {
for await (let chunk of ints()) {
await writableStreamDefaultWriter.ready;
writableStreamDefaultWriter.write(chunk);
}
writableStreamDefaultWriter.close();
})();*/




// 通过管道连接流
/*async function* ints() {
// 每1000 毫秒生成一个递增的整数
for (let i = 0; i < 5; ++i) {
yield await new Promise((resolve) => setTimeout(resolve, 1000, i));
}
}
const integerStream = new ReadableStream({
async start(controller) {
for await (let chunk of ints()) {
controller.enqueue(chunk);
}
controller.close();
}
});
const doublingStream = new TransformStream({
transform(chunk, controller) {
controller.enqueue(chunk * 2);
}
});
// 通过管道连接流
const pipedStream = integerStream.pipeThrough(doublingStream);
// 从连接流的输出获得读取器
const pipedStreamDefaultReader = pipedStream.getReader();
// 消费者
(async function() {
while(true) {
const { done, value } = await pipedStreamDefaultReader.read();
if (done) {
break;
} else {
console.log(value);
}
}
})();*/




//将ReadableStream 连接到WritableStream
/*async function* ints() {
// 每1000 毫秒生成一个递增的整数
for (let i = 0; i < 5; ++i) {
yield await new Promise((resolve) => setTimeout(resolve, 1000, i));
}
}
const integerStream = new ReadableStream({
async start(controller) {
for await (let chunk of ints()) {
controller.enqueue(chunk);
}
controller.close();
}
});
const writableStream = new WritableStream({
write(value) {
console.log(value);
}
});
const pipedStream = integerStream.pipeTo(writableStream);*/



// 一次性向页面中插入多个元素
/*const fragment = new DocumentFragment();
const foo = document.querySelector('#foo');
// 为DocumentFragment 添加子元素不会导致布局重排
fragment.appendChild(document.createElement('p'));
fragment.appendChild(document.createElement('p'));
fragment.appendChild(document.createElement('p'));
console.log(fragment.children.length); // 3
foo.appendChild(fragment);
console.log(fragment.children.length); // 0
console.log(document.body.innerHTML);
*/


// 影子DOM
/*for (let color of ['red', 'green', 'blue']) {
const div = document.createElement('div');
const shadowDOM = div.attachShadow({ mode: 'open' });   //影子DOM，不可见
document.body.appendChild(div);        //将影子DOM加入网页中，渲染出来
shadowDOM.innerHTML = `
<p>Make me ${color}</p>
<style>
p {
color: ${color};
}
</style>
`;
}*/




/*document.body.innerHTML = `
<div>
<p>Foo</p>
</div>
`;
setTimeout(() => document.querySelector('div').attachShadow({ mode: 'open' }), 1000);  //1秒后设为空的影子DOM，网页不可见
*/



// 自定义通用元素
/*class FooElement extends HTMLElement {}
customElements.define('x-foo', FooElement);         //创建x-foo自定义标签
document.body.innerHTML = `
<x-foo>I'm inside a nonsense element.</x-foo >
`;
console.log(document.querySelector('x-foo') instanceof FooElement); // true*/



// 自定义继承HTML元素
/*class FooElement extends HTMLElement {
constructor() {
super();
console.log('x-foo')
}
}
customElements.define('x-foo', FooElement);
document.body.innerHTML = `
<x-foo></x-foo>
<x-foo></x-foo>
<x-foo></x-foo>
`;


// 自定义继承Div元素
class FooElement extends HTMLDivElement {
constructor() {            //构造函数
super();
console.log('x-foo')
}
}
customElements.define('x-foo', FooElement, { extends: 'div' });
document.body.innerHTML = `
<div is="x-foo"></div>      
<div is="x-foo"></div>
<div is="x-foo"></div>
`;          //is属性表示div元素为x-foo的实例
*/




/*const template = document.querySelector('#x-foo-tpl');
class FooElement extends HTMLElement {
constructor() {
super();
this.attachShadow({ mode: 'open' });
this.shadowRoot.appendChild(template.content.cloneNode(true));          //在自定义元素中添加影子DOM
}
}
customElements.define('x-foo', FooElement);
document.body.innerHTML += `<x-foo></x-foo`;
*/




/*throw new SyntaxError("I don't like your syntax.");
throw new InternalError("I can't do that, Dave.");
throw new TypeError("What type of variable do you take me for?");
throw new RangeError("Sorry, you just don't have the range.");
throw new EvalError("That doesn't evaluate.");
throw new URIError("Uri, is that you?");
throw new ReferenceError("You didn't cite your references properly.");*/



// 调试器
/*console.log("dsdfgh");
debugger;      // 调试器
console.log("dsffgbhtrht");
console.log("12345678");*/



// 页面显示日志信息
/*function log(message) {
// 这个函数的词法作用域会使用这个实例
// 而不是window.console
const console = document.getElementById("output");
if (console === null){
console = document.createElement("div");
console.id = "debuginfo";
console.style.background = "#dedede";
console.style.border = "1px solid silver";
console.style.padding = "5px";
console.style.width = "400px";
console.style.position = "absolute";
console.style.right = "0px";
console.style.top = "0px";
document.body.appendChild(console);
}
console.innerHTML += '<p> ${message}</p>';

}
log("hello world");
*/


// condition为FAIL时抛出异常消息
/*function assert(condition, message) {
if (!condition) {
throw new Error(message);
}
}*/




/*let parser = new DOMParser();
let xmldom = parser.parseFromString("<root><child/></root>", "text/xml");     //XML字符解析为DOM结构
console.log(xmldom.documentElement.tagName); // "root"
console.log(xmldom.documentElement.firstChild.tagName); // "child"
let anotherChild = xmldom.createElement("child");
xmldom.documentElement.appendChild(anotherChild);
let children = xmldom.getElementsByTagName("child");
console.log(children.length); // 2

let serializer = new XMLSerializer();
let xml = serializer.serializeToString(xmldom);    //把XMLDOM文档转换为XML字符
console.log(xml);


let supportsXPath = document.implementation.hasFeature("XPath", "3.0");  //确定浏览器是否支持DOM Level 3 XPath


let result = xmldom.evaluate("employee/name", xmldom.documentElement, null,
XPathResult.ORDERED_NODE_ITERATOR_TYPE, null);     //XPath 表达式、上下文节点、命名空间解析器、返回的结果类型和XPathResult 对象
if (result !== null) {
let element = result.iterateNext();       //返回匹配到的节点
while(element) {
console.log(element.tagName);
node = result.iterateNext();
}
}*/





/*let book = {
title: "Professional JavaScript",
authors: [
"Nicholas C. Zakas",
"Matt Frisbie"
],
edition: 4,
year: 2017
}; 
let jsonText = JSON.stringify(book);     // 将javascript序列化为JSON字符串
let jsonText = JSON.stringify(book, ["title", "edition"]);    // 将javascript序列化为JSON字符串，其中只包含title和edition两个属性
let jsonText = JSON.stringify(book, (key, value) => {      //将javascript序列化为JSON字符串，精细控制序列化时执行的动作
switch(key) {
case "authors":
return value.join(",")
case "year":
return 5000;
case "edition":
return undefined;
default:
return value;
}
});
let jsonText = JSON.stringify(book, null, 4);     // 将javascript序列化为JSON字符串，格式化数据结构每级缩进4个空格
let bookCopy = JSON.parse(jsonText);     // 将JSON字符串解析为javascript值*/




/*let book = {
title: "Professional JavaScript",
authors: [
"Nicholas C. Zakas",
"Matt Frisbie"
],
edition: 4,
year: 2017,
toJSON: function() {
return this.title;
}
};
let jsonText = JSON.stringify(book);  //序列化之前先通过toJSON过滤字符
console.log(jsonText);*/




/*let book = {
title: "Professional JavaScript",
authors: [
"Nicholas C. Zakas",
"Matt Frisbie"
],
edition: 4,
year: 2017,
releaseDate: new Date(2017, 11, 1)
};
let jsonText = JSON.stringify(book);
let bookCopy = JSON.parse(jsonText,
    (key, value) => key == "releaseDate" ? new Date(value) : value);       // 将JSON字符串解析为javascript值
    alert(bookCopy.releaseDate.getFullYear());*/




// 期约案例
/*function red() {
  console.log('red');
}

function green() {
  console.log('green');
}

function yellow() {
  console.log('yellow');
}

let light = (fn, timer) => new Promise(resolve => {
  setTimeout(function() {
    fn()
    resolve()
  }, timer)
})

// times为交替次数
function start(times) {
  if (!times) {
    return
  }

  times--
  Promise.resolve()
    .then(() => light(red, 3000))
    .then(() => light(green, 1000))
    .then(() => light(yellow, 2000))
    .then(() => start(times))
}
start(3)*/




// 网络请求
/*let xhr = new XMLHttpRequest();
xhr.onreadystatechange = function() {    //请求过程的阶段变化时触发
if (xhr.readyState == 4) {       //0表示未初始化，1表示已打开，2表示已发送，3表示接收中，4表示已完成 
if ((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304) {
alert(xhr.responseText);
} else {
alert("Request was unsuccessful: " + xhr.status);
}
}
};
xhr.open("get", "example.php", true);   //向php页面发送异步get请求
xhr.setRequestHeader("MyHeader", "MyValue");     //设置自定义请求头
xhr.send(null);   //发送请求体
let myHeader = xhr.getResponseHeader("MyHeader");    //获取指定的请求头
let allHeaders xhr.getAllResponseHeaders();         //获取所有的请求头
xhr.abort();    //取消请求*/



//GET请求查询字符串编码
/*function addURLParam(url, name, value) {
url += (url.indexOf("?") == -1 ? "?" : "&");
url += encodeURIComponent(name) + "=" + encodeURIComponent(value);     //对查询字符串进行编码，请求发送前都需要对参数名和参数值进行编码
return url;
}
let url = "example.php";
// 添加参数
url = addURLParam(url, "name", "Nicholas");
url = addURLParam(url, "book", "Professional JavaScript");
// 初始化请求
xhr.open("get", url, false);*/



//以表单形式发送请求
/*function submitData() {
let xhr = new XMLHttpRequest();
xhr.onreadystatechange = function() {
if (xhr.readyState == 4) {
if ((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304) {
alert(xhr.responseText);
} else {
alert("Request was unsuccessful: " + xhr.status);
}
}
};
xhr.open("post", "postexample.php", true);
xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");   //设置发送数据类型为表单类型
let form = document.getElementById("user-info");
xhr.send(serialize(form));     //将form元素序列化作为请求体发送
}*/



//以表单形式发送请求
/*let xhr = new XMLHttpRequest();
xhr.onreadystatechange = function() {
if (xhr.readyState == 4) {
if ((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304) {
alert(xhr.responseText);
} else {
alert("Request was unsuccessful: " + xhr.status);
}
}
};
xhr.open("post", "postexample.php", true);
let form = document.getElementById("user-info");
xhr.send(new FormData(form));         //FormData序列化整个表单，并以表单类型发送请求，而不需要额外设置请求数据类型
*/



// 请求超时时间设置
/*let xhr = new XMLHttpRequest();
xhr.onreadystatechange = function() {
if (xhr.readyState == 4) {
try {
if ((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304) {
alert(xhr.responseText);
} else {
alert("Request was unsuccessful: " + xhr.status);
}
} catch (ex) {
// 假设由ontimeout 处理
}
}
};
xhr.open("get", "timeout.php", true);
xhr.timeout = 1000; // 设置1 秒超时
xhr.ontimeout = function() {    //超时时触发，同时也会触发onreadystatechange事件
alert("Request did not return in a second.");
};
xhr.overrideMimeType("text/xml");   //重写响应的数据类型
xhr.send(null);*/


// 展示请求响应进度条
/*let xhr = new XMLHttpRequest();
xhr.onload = function(event) {          //响应接收完成后触发
    if ((xhr.status >= 200 && xhr.status < 300) ||
        xhr.status == 304) {
        alert(xhr.responseText);
} else {
    alert("Request was unsuccessful: " + xhr.status);
}
};
xhr.onprogress = function(event) {    //响应过程中持续触发
    let divStatus = document.getElementById("status");
    if (event.lengthComputable) {        //表示响应进度条是否可用
        divStatus.innerHTML = "Received " + event.position + " of " +         //事件响应是当前进度与总进度
        event.totalSize +
        " bytes";
    }
};
xhr.open("get", "altevents.php", true);
xhr.send(null);*/


// 预检请求
//浏览发送请求头部
/*Origin: http://www.nczonline.net            //请求域名，与服务器相同则可以访问服务器
Access-Control-Request-Method: POST        //请求希望使用的方法
Access-Control-Request-Headers: NCZ        //用逗号分隔的自定义头部列表
*/
//服务器响应头部
/*Access-Control-Allow-Origin：与简单请求相同。
Access-Control-Allow-Methods：允许的方法（逗号分隔的列表）。
Access-Control-Allow-Headers：服务器允许的头部（逗号分隔的列表）。
Access-Control-Max-Age：缓存预检请求的秒数。
Access-Control-Allow-Credentials: true         //表示服务器允许浏览器发送请求凭据
*/


//图片探测
/*let img = new Image();
img.onload = img.onerror = function() {
alert("Done!");
};
利用任何页面都可以跨域加载图片而不必担心限制，可以图片形式发送请求，虽然浏览器拿不到服务器任何数据，但可以根据响应浏览器做出动作
img.src = "http://www.example.com/test?name=Nicholas";   */



//网络请求
/* $.get("http://169.254.200.238:8080/jsonp.do", function (data) {
     console.log(data);
 });*/



/* fetch('/hangs-forever')
.then((response) => {
console.log(response);
}, (err) => {console.log(err);
});*/


/*fetch('/sfe')
.then((resolve)=>{consonle.log("ok")},(reject)=>{console.log("NG1")});*/



//fetch发送POST请求
/* let payload = JSON.stringify({
"foo": "bar"
});
let jsonHeaders = new Headers({
'Content-Type': 'application/json'
});
fetch('/send-me-json', {
method: 'POST', // 发送请求体时必须使用一种HTTP 方法
body: payload,
headers: jsonHeaders
});*/


// 在请求体中发送参数
/*let payload = 'foo=bar&baz=qux';
let paramHeaders = new Headers({
'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
});
fetch('/send-me-params', {
method: 'POST', // 发送请求体时必须使用一种HTTP 方法
body: payload,
headers: paramHeaders
});*/


// 请求体发送文件
/*let imageFormData = new FormData();
let imageInput = document.querySelector("input[type='file']");
imageFormData.append('image', imageInput.files[0]);
fetch('/img-upload', {
method: 'POST',
body: imageFormData
});
//这个fetch()实现可以支持多个文件：
let imageFormData = new FormData();
let imageInput = document.querySelector("input[type='file'][multiple]");
for (let i = 0; i < imageInput.files.length; ++i) {
imageFormData.append('image', imageInput.files[i]);
}
fetch('/img-upload', {
method: 'POST',
body: imageFormData
});*/


// 加载Blob文件
/*const imageElement = document.querySelector('img');
fetch('my-image.png')
.then((response) => response.blob())   //期约解决返回一个Blob实例
.then((blob) => {
imageElement.src = URL.createObjectURL(blob);    //获取Blob路径加载到网页
});*/


// 中断停止请求
/*let abortController = new AbortController();
fetch('wikipedia.zip', { signal: abortController.signal })
.catch(() => console.log('aborted!');
// 10 毫秒后中断请求
setTimeout(() => abortController.abort(), 10);*/




/*let h = new Headers();
let m = new Map();
// 设置键
h.set('foo', 'bar');
m.set('foo', 'bar');
// 检查键
console.log(h.has('foo')); // true
console.log(m.has('foo')); // true
console.log(h.has('qux')); // false
console.log(m.has('qux')); // false
// 获取值
console.log(h.get('foo')); // bar
console.log(m.get('foo')); // bar
// 更新值
h.set('foo', 'baz');
m.set('foo', 'baz');
// 取得更新的值
console.log(h.get('foo')); // baz
console.log(m.get('foo')); // baz
// 删除值
h.delete('foo');
m.delete('foo');
// 确定值已经删除
console.log(h.get('foo')); // undefined
console.log(m.get('foo')); // undefined

let seed = [['foo', 'bar']];
let h = new Headers(seed);
let m = new Map(seed);
console.log(h.get('foo')); // bar
console.log(m.get('foo')); // bar*/



// 设置请求头
/*let h = new Headers();
h.append('foo', 'bar');
console.log(h.get('foo')); // "bar"
h.append('foo', 'baz');
console.log(h.get('foo')); // "bar"
*/


// fetch发送网络请求
/*let r = new Request('https://foo.com');
// 向foo.com 发送GET 请求
fetch(r);
// 向foo.com 发送POST 请求
fetch(r, { method: 'POST' });*/


// 一个request实例只能发送一次请求，再次发送前要clone一次请求体
/*let r = new Request('https://foo.com',
{ method: 'POST', body: 'foobar' });
// 3 个都会成功
fetch(r.clone());
fetch(r.clone());
fetch(r);*/


// Response响应
/*let r = new Response('foobar', {
status: 418,
statusText: 'I\'m a teapot'
});
console.log(r);*/


//text得到请求体
/*let request = new Request('https://foo.com',
{ method: 'POST', body: 'barbazqux' });
request.text()
.then(console.log);
// barbazqux
*/


//json得到请求体json格式
/*let request = new Request('https://foo.com',
{ method:'POST', body: JSON.stringify({ bar: 'baz' }) });
request.json()
.then(console.log);
// {bar: 'baz'}*/



//formData对象序列化作请求体
/*let myFormData = new FormData();
myFormData.append('foo', 'bar');
let request = new Request('https://foo.com',
{ method:'POST', body: myFormData });
request.formData()
.then((formData) => console.log(formData.get('foo'));
// bar
*/


//Body.arrayBuffer()以二进制形式显示请求体
/*let request = new Request('https://foo.com',
{ method:'POST', body: 'abcdefg' });
// 以整数形式打印二进制编码的字符串
request.arrayBuffer()
.then((buf) => console.log(new Int8Array(buf)));
// Int8Array(7) [97, 98, 99, 100, 101, 102, 103]*/



//以blob()形式显示请求体
/*let request = new Request('https://foo.com',
{ method:'POST', body: 'abcdefg' });
request.blob()
.then(console.log);
// Blob(7) {size: 7, type: "text/plain;charset=utf-8"}*/



/*function takeLongTime() {
 return new Promise(resolve => {
 setTimeout(() => resolve("long_time_value"), 1000);
 });
}*/
/*function takeLongTime() {
 return new Promise(resolve => resolve("long_time_value"));
}
async function test() {
 const v = await takeLongTime();
 console.log(v);
}
test();*/



/*
let a =3;
let b= 5;
let p = new Promise((resolve, reject) => {
  let c = a+b;
  if (c>10) {
    resolve()
  } else {
    reject()
  }
})

p.then(() => {
    console.log("OK");
}, () => {
    console.log("NG");
})*/



/*function takeLongTime() {
    return new Promise(resolve => {
        setTimeout(() => resolve("long_time_value"), 1000);
    });
}

async function test() {
    const v = takeLongTime();
    await sleep(2000);
    console.log(v);  // 一秒钟后输出long_time_value
}

function sleep(ms) {
    setTimeout(()=>console.log(""), ms);
  return;
}

test();*/


//Beacon API发送请求
// navigator.sendBeacon('https://example.com/analytics-reporting-url', '{foo: "bar"}');


/*// Web Socket网络连接发送数据
let socket = new WebSocket("ws://www.example.com/server.php");
let stringData = "Hello world!";
let arrayBufferData = Uint8Array.from(['f', 'o', 'o']);
let blobData = new Blob(['f', 'o', 'o']);
socket.send(stringData);
socket.send(arrayBufferData.buffer);
socket.send(blobData);

// socket接收数据
socket.onmessage = function(event) {
let data = event.data;
// 对数据执行某些操作
};*/



// cookie辅助函数读写删除
/*class CookieUtil {
    static get(name) {
        let cookieName = `${encodeURIComponent(name)}=`,
        cookieStart = document.cookie.indexOf(cookieName),
        cookieValue = null;
        if (cookieStart > -1){
            let cookieEnd = document.cookie.indexOf(";", cookieStart);
            if (cookieEnd == -1){
                cookieEnd = document.cookie.length;
            }
            cookieValue = decodeURIComponent(document.cookie.substring(cookieStart
                + cookieName.length, cookieEnd));
        }
        return cookieValue;
    }
    static set(name, value, expires, path, domain, secure) {
        let cookieText =
        `${encodeURIComponent(name)}=${encodeURIComponent(value)}`
        if (expires instanceof Date) {
            cookieText += `; expires=${expires.toGMTString()}`;
        }
        if (path) {
            cookieText += `; path=${path}`;
        }
        if (domain) {
            cookieText += `; domain=${domain}`;
        }
        if (secure) {
            cookieText += "; secure";
        }
        document.cookie = cookieText;
    }
    static unset(name, path, domain, secure) {
        CookieUtil.set(name, "", new Date(0), path, domain, secure);   //把过期时间设置为之前，用于删除cookie
    }
};*/



// name=name1=value1&name2=value2&name3=value3&name4=value4&name5=value5
// 子cookie辅助函数
/*class SubCookieUtil {
    static get(name, subName) {
        let subCookies = SubCookieUtil.getAll(name);
        return subCookies ? subCookies[subName] : null;
    }
    static getAll(name) {
        let cookieName = encodeURIComponent(name) + "=",
        cookieStart = document.cookie.indexOf(cookieName),
        cookieValue = null,
        cookieEnd,
        subCookies,
        parts,
        result = {};
        if (cookieStart > -1) {
            cookieEnd = document.cookie.indexOf(";", cookieStart);
            if (cookieEnd == -1) {
                cookieEnd = document.cookie.length;
            }
            cookieValue = document.cookie.substring(cookieStart +
                cookieName.length, cookieEnd);
            if (cookieValue.length > 0) {
                subCookies = cookieValue.split("&");
                for (let i = 0, len = subCookies.length; i < len; i++) {
                    parts = subCookies[i].split("=");
                    result[decodeURIComponent(parts[0])] =
                    decodeURIComponent(parts[1]);
                }
                return result;
            }
        }
        return null;
    }
// 省略其他代码
};*/


// 格灵威治时间，0时区时间
/*var d = new Date()
console.log(d.toGMTString())
console.log(d.toUTCString())
*/


/*Web Storage存储对象
clear()删除所有值，不在Firefox实现
getItem(name)取得给定name的值
key(index)取得给定数值位置的名称
removeItem(name)删除给定name的名/值对
setItem(name,value)设置给定name的值
sessionStorage对象存储会话数据，浏览器关闭数据会消失
localStorage对象在客户端持久存储数据

window.addEventListener("storage",
(event) => alert('Storage changed for ${event.domain}'));  //Web Storage存储对象发生改变触发此事件*/



/*
let user = {
username: "007",
firstName: "James",
lastName: "Bond",
password: "foo"
};*/

//let transaction = db.transaction();     //对所有数据库对象有只读权限
//let transaction = db.transaction("users");     //对指定数据库对象有只读权限
//let transaction = db.transaction(["users", "anotherStore"]);        //对指定的多个数据库对象有只读权限
//let transaction = db.transaction("users", "readwrite");   //对指定数据库对象有读写权限，readonly/readwrite/versionchange

/*const transaction = db.transaction("users"),    //指定数据库对象有只读权限
store = transaction.objectStore("users"),      //通过键访问数据库对象实例
request = store.get("007");  //get获取数据库对象   put()更新对象   delete()删除对象   clear()删除所有对象
request.onerror = (event) => alert("Did not get the object!");
request.onsuccess = (event) => alert(event.target.result.firstName);*/

/*transaction.onerror = (event) => {
// 整个事务被取消时触发
};
transaction.oncomplete = (event) => {
// 整个事务成功完成时触发
};*/
/*var db;
var request = window.indexedDB.open("user", 1);
// 数据库创建或升级时触发
request.onupgradeneeded = (event) => {
  // 通过事件对象的target.result属性，拿到数据库实例。
  db = event.target.result;
  console.log('onupgradeneeded');

  var objectStore;
  if (!db.objectStoreNames.contains("user")) {
    // 创建对象仓库，简单说就是建立一张表
    objectStore = db.createObjectStore("user", { keyPath: "username" });
  }
};
// 数据打开时触发；第一次打开数据库时，会先触发upgradeneeded事件，然后触发success事件。
request.onsuccess = (event) => {
  // 通过request对象的result属性拿到数据库对象。
  db = request.result;
  console.log("success");
};
// 失败时触发
request.onerror = (event) => {
  console.log(error);
};

var requestt = db.transaction(["user"], "readwrite")
    .objectStore("user")
    .add(user);
  requestt.onsuccess = function (event) {
    console.log('数据写入成功');
  };
  requestt.onerror = function (event) {
    console.log('数据写入失败');
  }*/


/*只要使用某个object 属性超过一次，就应该将其保存在局部变量中
let url = window.location.href;
let query = url.substring(url.indexOf("?"));*/

console.log('数据写入失败');