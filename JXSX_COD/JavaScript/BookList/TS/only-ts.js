"use strict";
// 设定参数类型
/* function greet(person: string, date: Date) {
  console.log(`Hello ${person}, today is ${date.toDateString()}!`);
}
 
greet("Maddison", new Date());
*/
/* //剩余参数，不定参数
function buildName(name1:string,...restName:string[]):string
{
  // return name1 + " " + restName.join(" ");
  // return name1 + " " + restName[1];
}
console.log(buildName("aaa","b","c","d")); */
// 函数返回值类型
/*
function getFavoriteNumber(): number {
  return 26;
}
const a = getFavoriteNumber();
console.log(a);
*/
//类型推断
/*
const names = ["Alice", "Bob", "Eve"];
//函数的上下文类型推断
names.forEach(function (s) {
  console.log(s.toUpperCase());
});
 names.forEach(s=> {
  console.log(s.toUpperCase());
});
*/
// function printCoord(pt: { x: number; y: number }) {
//   console.log("坐标的 x 值是 " + pt.x);
//   console.log("坐标的 y 值是 " + pt.y);
// }
// printCoord({ x: 3, y: 7 });
//?可选参数
/*
function printName(obj: { first: string; last?: string }) {
 if(obj.last ===undefined)
  console.log(obj.first);
  else
  console.log(obj.first + obj.last);
}
printName({ first: "Bob" });
printName({ first: "Alice", last: "Alisson" });
*/
//联合类型, 类型可选
/*
function printId(id: number | string) {
  if (typeof id === "string") {
    // 在这个分支中，id 的类型是 'string'
    console.log(id.toUpperCase());
  } else {
    // 在这里，id 的类型是 'number'
    console.log(id);
  }
}
printId("sajfe");
*/
/*function welcomePeople(x: string[] | string) {
  if (Array.isArray(x)) {
    // 在这里：'x' 的类型是 'string[]'
    console.log("你好，" + x.join(" 和 "));
  } else {
    // 在这里：'x' 的类型是 'string'
    console.log("欢迎，孤独旅行者 " + x);
  }
}
let x = ["a","b","c"];
let y = "ssss";
welcomePeople(y);*/
/* //type类型别名
// type str = string;
//联合类型
type str = string | number;
let a:str="aaaaa";
console.log(a); */
/* //变量多类型
let a :string|null|number;
a = "aa";
a = null;
a = 12; */
/*
// 交叉类型
type Point = {
  x: number;
  y: number;
};
// 与前面的示例完全相同
function printCoord(pt: Point) {
  console.log("x 的坐标值是 " + pt.x);
  console.log("y 的坐标值是 " + pt.y);
}
printCoord({ x: 100, y: 100 });
*/
//字面量类型：限制变量只能取特定的值
/* type n = "a" | "b" |"c"|4;
let na:n = "aa";
console.log(na); */
// type UserRecord = User & WithTimestamp; // 同时具有 User 和 WithTimestamp 的所有属性
/* // 函数类型别名
type UnknownRecord = Record<string, unknown>;
function logRecord(record: UnknownRecord) {
 for (const key in record) {
   console.log(`${key}:`, record[key]);
 }
}
const config: UnknownRecord = { theme: "dark", version: 1 };
logRecord(config); */
// 类型别名，可实现类似泛型的功能
/* type Words = 'a'|'b'|"c";
type W<T> = T extends Words ? true : false;   //如果T如果赋值给Word,则W<T>的类型为ture,否则为false
type WA = W<'a'>; // -> true    //类型别名，WA类型只能被赋予ture这一个值
type WD = W<'d'>; // -> false
let w:WA = true;
console.log(w); */
//接口
/* face Point {
  x: number;
  y: number;
}
function printCoord(pt: Point) {
  console.log("x 的坐标值是 " + pt.x);
  console.log("y 的坐标值是 " + pt.y);
  console.log(pt.add(pt.x,pt.y));
}
printCoord({ x: 100, y: 100 }); */
// 参数及返回值类型
/* function greet(name:string):string
{
  return "hello " + name.toUpperCase();
}
console.log(greet("张三")); */
/* function greet(name:string):void
{
  console.log("name:"+name);
}
greet("he") */
/* let vara:any[][] = [];    //二维数组
let var1:number[]=[1,2,3,4,5];   //number数组
let var2:Array<string> = ["aa","bb","cc"];  //string数组等同于上面的写法
let var3:boolean[] = [false];   //布尔数组
let var4:[string,number]=["张三",12];   //元组，就是固定长度的数组
let var5:[string,string,number] = ["aa","bb",12];    //元组，就是固定长度的数组
vara.push(var1);
vara.push(var2);
vara.push(var3);
vara.push(var4);
vara.push(var5);
for(let i of vara)
{
  console.log(i);
} */
/* // 可选参数
function show(name:string,age?: string):void{
  let msg = age?name +" "+ age:name;
  console.log(msg);
}
show("li");
show("he","12") */
/* // 默认参数
function show(name:string,age:number=12):void
{
  console.log(name + age);
}
show("张三");
show("李四",10); */
/* //定义接口：描述用户对象的结构,只规定了最小的结构，实参可以在这基础上拓展
interface jie{
  name:string,
  age:number,
  isCheck:boolean
}
function show(a:jie):void  //引用接口
{
  console.log(a);
}
let a={name:"张三",age:22,isCheck:false};
show(a);
// 接口继承
interface jieEx extends jie{
    permissions: string[];
}
function showjieEx(a:jieEx):void   //引用接口
{
  console.log(a);
}
let b={name:"李四",age:23,isCheck:true,permissions:["aa","bb"]};  //jie接口实参在接口结构上拓展了permissions属性
show(b);
let c={name:"李四",age:23,isCheck:true,permissions:["aa","bb"]};   //引用继承接口
showjieEx(c) */
/* //类实现接口
interface Person {
  name: string;
  age: number;
  greet(): void;
  greet:()=>void;   //同上
}
class Student implements Person {
  constructor(public name: string, public age: number) {}
  greet() {
    console.log(`Hello, my name is ${this.name}`);
  }
}
let stu = new Student("张三",12);
stu.greet(); */
/* //枚举类型
enum typeEn{
  aaa,bbb,ccc
}
let a = typeEn.bbb
console.log(a); */
/* // 访问修饰符,不写时默认为public
class Person {
  public name: string;
  protected age: number;
  private sex:string;
  readonly aa:number;
  public constructor(name: string, age: number,sex:string,aa:number) {
    this.name = name;
    this.age = age;
    this.sex = sex;
    this.aa = aa;
  }
  public printSex():string
  {
    return this.sex;
  }
}
let per = new Person("张三",12,"male",34567);
console.log(per)
console.log(per.printSex())
console.log(per.name)
// console.log(per.age)
// per.aa = 9;
console.log(per.aa); */
/* class Person{
  // 在构造函数参数上直接使用修饰符
  // 相当于同时声明属性并赋值
  // 自动创建name和age属性
  constructor(public name:string,public age:number){}
}
let per = new Person("张三",11);
console.log(per);
console.log(per.name); */
/* abstract class Animal {
  abstract makeSound(): void;   //抽象方法
  show():void   //类里方法不能加function
  {
    console.log("Animal name");
  }
}
class Dog extends Animal {
  makeSound() {    //实现抽象方法
    console.log("Woof!");
  }
}
let d = new Dog()
d.makeSound();
d.show(); */
/* //泛型
function identity<T>(value: T): T {
  return value;
}
let num = identity<number>(42);
console.log(num);
let str = identity<string>("hello");
console.log(str); */
/* function show<t>(name:t):t
{
  return name;
}
console.log(show("he"));
console.log(show<string>("he")); */
/* //类型守卫
function printId(id: string | number) {
  if (typeof id === "string") {
    console.log(id.toUpperCase());
  } else {
    console.log(id.toFixed(2));
  }
}
printId("Name");
printId(23); */
/* // TypeScript 增加了 JavaScript 的可选链 (?.) 和空值合并运算符 (??)，简化了代码中对可能为 null 或 undefined 值的处理。let user = { name: "Alice", address: { city: "Wonderland" } };
console.log(user?.address?.city); // 如果 address 存在则输出 city，否则返回 undefined
let value = null;
console.log(value ?? "default"); // 如果 value 为 null 或 undefined，则返回 "default" */
/* //for in适合遍历对象，不适合用于数组
// for in 返回迭代对象的键列表，for of 返回迭代对象的值列表
// for..in 可作用于任何对象，它可用于检查对象上的属性；而 for..of 主要关注可迭代对象的值
let a = [1,2,3,4,5]
for(let i of a)
{
  console.log(i);
} */
/* // 箭头函数，Lambda表达式
const greet = (name: string): string => "Hello, " + name;
console.log(greet("aa"));
const greett = (name:string):string =>{return name + " hello";}
console.log(greett("aa")); */
/* // nerver表示没有返回值，通常用于抛出错误或进入无限循环的函数，表示该函数永远不会正常结束
function throwError(message: string): never {
  throw new Error(message);
} */
/* // 与 any 类似，但更严格。必须经过类型检查后才能赋值给其他类型变量。
// unknown 是类型安全的 any。使用 unknown 时，必须先进行类型检查才能使用。
let value: unknown = "Hello";
if (typeof value === "string") {  //必须要有typeof类型检查
  let message: string = value;
} */
/* // 类型断言，类型转换,必须先转换为any再转换为其它类型
let a = 12;
// let b:string =<string><any>a;
let c:string = (a as any) as string;  //效果同上 */
/* function addN(count:number):number
{
  let b :number = 0;
  //for循环
  for(let i =0;i<10;i++)
  {
    b += i;
  }
  return b;
}
console.log(addN(10)); */
/* for in 循环
let a:number[]= [1,2,3,4,5,6]
for(let i in a)
{
  console.log(i);
} */
/* let list = [4, 5, 6];
list.forEach((val, idx, array) => {
    // val: 当前值
    // idx：当前index
    // array: Array
    console.log(array);
}); */
/* let list = [4, 5, 6];
list.every((val, idx, array) => {
    // val: 当前值
    // idx：当前index
    // array: Array
    return true; // Continues
    // Return false will quit the iteration
}); */
/* // 匿名函数自调用
(function show(){
  console.log("hello");
})(); */
/* // 构造函数来定义函数
var myFunction = new Function("a", "b", "return a * b");
var x = myFunction(4, 3);
console.log(x); */
/* // 定义函数，函数调用必须要符合此定义
function disp(s1:string):void;
function disp(n1:number,s1:string):void;
// 函数重载，用any替代参数类型
function disp(x:any,y?:any):void {
    console.log(x);
    console.log(y);
}
disp("abc")
disp(1,"he"); */
/* // 生成器函数：使用 function* 语法
function* numberGenerator() {
    yield 1;  // 暂停并返回 1
    yield 2;  // 暂停并返回 2
    yield 3;  // 暂停并返回 3
}
// 创建生成器实例
var gen = numberGenerator();
// 每次调用 next() 都会执行到下一个 yield
console.log("第一个: " + gen.next().value);
console.log("第二个: " + gen.next().value);
console.log("第三个: " + gen.next().value);
console.log("完成: " + gen.next().done); */
/* // Promise代表一个异步操作的执行结果
// async 函数自动返回 Promise
async function greet(): Promise<string> {
    return "Hello, World!";
}
greet().then(function(result) {
    console.log("结果: " + result);
});
// 异步函数返回 Promise
async function getData() {
    return { name: "Alice", age: 25 };
}
getData().then(function(data) {
    console.log("数据: " + JSON.stringify(data));
}); */
/* // 模拟异步操作
function delay(ms: number): Promise<string> {
    return new Promise(function(resolve) {
        setTimeout(function() {
            resolve("完成!");
        }, ms);
    });
}
async function main() {
    console.log("开始...");
    var result = await delay(2000);
    console.log("结果: " + result);
    console.log("结束");
}
main(); */
/* // 异步并行
function fetchUser(id: number): Promise<{ id: number; name: string }> {
    return Promise.resolve({ id: id, name: "User" + id });
}
async function main() {
    // 串行执行
    console.time("串行");
    var user1 = await fetchUser(1);
    var user2 = await fetchUser(2);
    console.log("串行完成: " + user1.name + ", " + user2.name);
    console.timeEnd("串行");
    // 并行执行
    console.time("并行");
    var results = await Promise.all([fetchUser(1), fetchUser(2)]);   //all等待所以方法都执行完成，返回一个包含所有结果的数组
    console.log("并行完成: " + results[0].name + ", " + results[1].name);
    console.timeEnd("并行");
}
main(); */
/* // 创建 Promise，使用泛型指定解决值的类型
// Promise<string> 表示成功时返回字符串
var promise = new Promise<string>(function(resolve, reject) {
    var success = true;
    if (success) {
        // 调用 resolve 表示操作成功，传入结果值
        resolve("成功!");
    } else {
        // 调用 reject 表示操作失败，传入错误
        // reject(new Error("失败"));
        reject("失败");
    }
});
// 使用 then 处理成功情况
promise.then(function(value) {
    console.log("完成: " + value);
})
["catch"](function(error) {
    // 使用 catch 处理失败情况
    console.log("错误: " + error.message);
}); */
/* // Promise链式调用：每个 then 返回新的值，被下一个 then 接收
var promise = Promise.resolve(1)
    .then(function(n) {
        // 第一个 then，n = 1
        return n * 2;  // 返回 2
    })
    .then(function(n) {
        // 第二个 then，n = 2
        return n + 10;  // 返回 12
    })
    .then(function(n) {
        // 第三个 then，n = 12
        console.log("最终结果: " + n);
        return n;
    });
console.log("Promise 链: " + promise); */
/* // 创建三个 Promise
var p1 = Promise.resolve(1);
var p2 = Promise.resolve(2);
var p3 = Promise.resolve(3);
// Promise.all 等待所有 Promise 完成
// 返回一个数组，包含所有 Promise 的结果
// Promise.all([p1, p2, p3]).then(function(results) {
//     console.log("全部完成: " + results);
//     // 计算总和
//     console.log("总和: " + results.reduce(function(a, b) { return a + b; }, 0));
// });
Promise.all([p1,p2,p3]).then((res)=>{
  console.log(res);
  //reduce用于将数组元素按顺序“缩减”为单个值，通过累加器函数处理每个元素
  // acc是累计器，保存上一次回调的返回值，cur表示当前处理的数组元素
  console.log("总和："+res.reduce((acc,cur)=>{return acc+cur;},0));
}) */
/* // 创建三个不同延迟的 Promise
var p1 = new Promise(function(resolve) {
    setTimeout(function() { resolve("p1"); }, 100);
});
var p2 = new Promise(function(resolve) {
    setTimeout(function() { resolve("p2"); }, 50);
});
var p3 = new Promise(function(resolve) {
    setTimeout(function() { resolve("p3"); }, 10);
});
// Promise.race 返回最先完成的 Promise 的结果
Promise.race([p1, p2, p3]).then(function(value) {
    console.log("最先完成: " + value);
}); */
/* let a:number = 100000.22;
console.log(a.toExponential())   //把对象的值转换为指数计数法
console.log(a.toFixed(1))  //把数字转换为字符串，并对小数点指定位数
console.log(a.toLocaleString())    //把数字转换为字符串，使用本地数字格式顺序
console.log(a.toPrecision(3))    //把对象的值转换为指定的长度，以指数计数法表示
console.log(a.toString())   //转换为字符串
console.log(a.toString(2))   //转换为字符串,以二进制表示 */
/* let a = new Array(5);
a[0] = "a";
a[1] = "b";
a[2] = "c";
a[3] = "d";
for(let b:string of a)
{
  console.log(b);
}
a = new Array("aa","bb","cc","dd","ee");
console.log(a);
let [z,x,c,v,n] = a;  //数组解构
console.log("z:"+z); */
/* let a = [1,2,3,4,5,6,7,8,9];
let b = a.map((x)=>{return x*2;})  //处理每个元素并返回值
console.log(b);
a.forEach((x)=>{console.log(x);})  //处理每个元素无返回值
b = a.filter((x)=>{return x>7;})   //过滤元素
console.log(b)
b = a.reverse();
console.log(b)
b = a.pop();   //删除最后一个元素并返回被删除的元素
console.log(b)
b = a.push(10)   //添加一个元素到到末尾
console.log(a); */
/* const map = new Map<string, number>();
map.set('one', 1);
map.set('two', 2);
console.log(map.get('one')); // 输出: 1
console.log(map.has('two')); // 输出: true
map.delete('one');
console.log(map.size); // 输出: 1
map.forEach((value, key) => {
  console.log(key, value); // 输出: two 2
});
console.log(map.size); // 输出: 0
 // 迭代 Map 中的 key
for (let key of map.keys()) {
    console.log(key);
}
 // 迭代 Map 中的 value
for (let value of map.values()) {
    console.log(value);
}
 // 迭代 Map 中的 key => value
for (let entry of map.entries()) {
    console.log(entry[0], entry[1]);
}
 // 使用对象解析
for (let [key, value] of map) {
    console.log(key, value);
}
map.clear();
console.log(map.size); // 输出: 0 */
/* enum ShapeKind {
    Circle = "circle",
    Square = "square"
}
interface Circle {
    kind: ShapeKind.Circle;
    radius: number;
}
interface Square {
    kind: ShapeKind.Square;
    sideLength: number;
}
var c: Circle = {
    kind: ShapeKind.Circle,
    radius: 10
};
console.log("圆形: " + JSON.stringify(c)); */
/* // 空值可选参数和属性
// 定义人员类型，地址是可选的
interface Person {
    name: string;
    address?: {
        city: string;
    };
}
// 创建人员对象，没有地址
var person: Person = { name: "Alice" };
// 可选链：安全访问可能不存在的属性
// 如果 address 不存在，返回 undefined 而不会报错
var city = person.address?.city;
// 结合空值合并运算符
console.log("城市: " + city);
console.log("城市: " + (person.address?.city ?? "未知")); */
/* // 创建 Symbol 作为属性键,生成唯一值，用途键名
var sym = Symbol("key");
// 使用 Symbol 作为对象的属性键
var obj = {
    name: "Alice",  // 普通字符串属性
    [sym]: "secret value"  // Symbol 属性，计算属性名
};
// 访问普通属性
console.log("普通属性: " + obj.name);
// 访问 Symbol 属性
console.log("Symbol 属性: " + obj[sym]);
// Symbol 属性不会出现在 JSON 中
console.log("对象: " + JSON.stringify(obj)); */
/* // for创建和获取全局Symbol，变量不存在则创建，已存在则获取已有的全局变量
var globalSym1 = Symbol.for("global");
var globalSym2 = Symbol.for("global");
// 相同 key 的 Symbol 是相等的
console.log("全局 Symbol 相等: " + (globalSym1 === globalSym2));
// // 获取 Symbol 的 key
console.log("Symbol key: " + Symbol.keyFor(globalSym1)); */
/* // 创建 Set，指定元素类型为 number
// Set自动去重
var numbers = new Set<number>();
// 添加元素
numbers.add(1);
numbers.add(2);
numbers.add(3);
numbers.add(1); // 重复值会被忽略，不会添加
// 检查大小和包含
console.log("Set 大小: " + numbers.size);
console.log("是否包含 2: " + numbers.has(2));
// 遍历 Set
numbers.forEach(function(value) {
    console.log("值: " + value);
});
// 转换为数组
var arr = Array.from(numbers);
console.log("转换为数组: " + arr); */
/* // 创建 Map，键类型为 string，值类型为 number
var map = new Map<string, number>();
// 设置键值对
map.set("one", 1);
map.set("two", 2);
map.set("three", 3);
// 获取值
console.log("获取 two: " + map.get("two"));
console.log("Map 大小: " + map.size);
console.log("是否包含 three: " + map.has("three"));
// 遍历 Map
map.forEach(function(value, key) {
    console.log(key + ": " + value);
});
// 转换为数组
console.log("转换为数组: " + Array.from(map.entries())); */
/* //无组
var mytuple = [42, "Runoob", true];
// 访问元组中的元素
var num = mytuple[0]; // 访问第一个元素，值为 42，类型为 number
var str = mytuple[1]; // 访问第二个元素，值为 "Runoob"，类型为 string
var bool = mytuple[2]; // 访问第三个元素，值为 true，类型为 boolean
console.log(num); // 输出: 42
console.log(str); // 输出: Runoob
console.log(bool); // 输出: true
// 添加符合类型的元素
mytuple.push("World"); // 合法，因为元组定义了可选的 string 类型
console.log(mytuple); // 输出: [42, "Hello", "World"]
let a = mytuple.pop();  //移除最后一个元素
console.log(a)
console.log(mytuple); */
/* //标签元组
let tuple: [id: number, name: string] = [1, "John"];
console.log(tuple[0]);
console.log(tuple[1]); */
/* //切片元组
let tuple: [number, string, boolean] = [42, "Hello", true];
let sliced = tuple.slice(1); // 从索引 1 开始切片
console.log(sliced); // 输出: ["Hello", true]
let array = Array.from(tuple);    //转换为普通数组
console.log(array); */
/* //接口定义及实现
interface IPerson {
    firstName:string,
    lastName:string,
    sayHi: ()=>string
}
var customer:IPerson = {
    firstName:"Tom",
    lastName:"Hanks",
    sayHi: ():string =>{return "Hi there"}
}
console.log("Customer 对象 ")
console.log(customer.firstName)
console.log(customer.lastName)
console.log(customer.sayHi())
var employee:IPerson = {
    firstName:"Jim",
    lastName:"Blakes",
    sayHi: ():string =>{return "Hello!!!"}
}
console.log("Employee  对象 ")
console.log(employee.firstName)
console.log(employee.lastName)
console.log(employee.sayHi()) */
/* //多继承接口
interface IParent1 {
    v1:number
}
interface IParent2 {
    v2:number
}
interface Child extends IParent1, IParent2 { }
var Iobj:Child = { v1:12, v2:23}
console.log("value 1: "+Iobj.v1+" value 2: "+Iobj.v2) */
/* // 定义 Car 类
class Car {
    // 字段：描述汽车的属性
    engine: string;
    // 构造函数：在创建对象时初始化 engine
    constructor(engine: string) {
        this.engine = engine;
    }
    // 方法：显示发动机信息
    disp(): void {
        console.log("发动机型号: " + this.engine);
    }
}
// 创建类的实例
var car = new Car("V8 发动机");
// 访问字段
console.log("读取发动机: " + car.engine);
// 调用方法
car.disp(); */
/* // 父类
class PrinterClass {
    doPrint(): void {
        console.log("父类的 doPrint() 方法");
    }
}
// 子类：重写父类方法,继承
class StringPrinter extends PrinterClass {
    doPrint(): void {
        // 调用父类的方法
        super.doPrint();
        // 子类自己的逻辑
        console.log("子类的 doPrint() 方法");
    }
}
var obj = new StringPrinter();
obj.doPrint(); */
/* // 类方法重载
class CC{
  //声明可以有很多，实现只能有一个，且要兼容所有声明
  greet(name:string):string;
  greet():void;
  greet(name?:string):void|string
  {
    if(typeof name==="undefined")
    {
      console.log("hello")
    }
    else if(typeof name === "string")
    {
      console.log( "name:"+name)
    }
  }
}
let c = new CC();
c.greet("John"); */
/* class Person {
}
var obj = new Person();
var isPerson = obj instanceof Person;   //instanceof用于判断对象是否是某个类的实例
console.log("obj 是 Person 类的实例吗？ " + isPerson); */
/* // 抽象类
abstract class Animal {
    abstract makeSound(): void;  // 抽象方法，子类必须实现
    move(): void {
        console.log("动物在移动");
    }
}
// 具体类：继承抽象类
class Dog extends Animal {
    makeSound(): void {
        console.log("汪汪汪!");
    }
    move():void{
      super.move();
      console.log("子类重写方法")
    }
}
var dog = new Dog();
dog.move();
dog.makeSound(); */
/* //自定义对象
let ac = {
  name:"张三",
  show:function(){console.log(this.name);},
  sayHi:function(){}
}
ac.show();
ac.sayHi = function(){console.log("hi");}
ac.sayHi(); */
/* // 基本语法
class Box<T> {
    private value: T;
    constructor(value: T) {
        this.value = value;
    }
    getValue(): T {
        return this.value;
    }
}
// 使用泛型类
let stringBox = new Box<string>("TypeScript");
console.log(stringBox.getValue()); // 输出: TypeScript */
/* // 泛型约束
interface Lengthwise {
    length: number;
}
function logLength<T extends Lengthwise>(arg: T): void {
    console.log(arg.length);
}
// 正确的使用
logLength("hello"); // 输出: 5
// 错误的使用，因为数字没有 length 属性
logLength(42); // 错误 */
/* //泛型与默认值
function defaultValue<T = string>(arg: T): T {
    return arg;
}
// 使用带默认值的泛型函数
let result1 = defaultValue("hello"); // 推断为 string 类型
let result2 = defaultValue(42);      // 推断为 number 类型
console.log(typeof result1);
console.log(typeof result2);
console.log(typeof result1); */
/* //命名空间
// 命名空间可以嵌套，在命名空间中再定义一个命名空间
namespace sp{
  export function show():void   //export表示可以在其它命名空间中调用此方法
  {
    console.log("hello");
  }
  export class CC{
    show()
    {
      console.log("命名空间中的类方法");
    }
  }
}
namespace ssp{
  sp.show();    //调用其它命名空间中的方法
  let c =new sp.CC();    //调用其它命名空间中的类方法
  c.show();
} */
/* //引用export-ts.ts文件中的命名空间，路径写编译后的js文件
import {exportTs} from './export-ts.js'
exportTs.show(); */
/* let a ={name:"张三",age:12};
console.log(a);
console.log(JSON.stringify(a)); */
/* // 泛型类型别名：定义结果类型
// Result<T> 是一个泛型类型，T 是成功时的数据类型
type Result<T> = { success: boolean; data?: T; error?: string };
// 泛型类型别名：定义键值对类型
// Pair<K, V> 有两个类型参数
type Pair<K, V> = { key: K; value: V };
// 使用泛型类型别名
var result: Result<string> = { success: true, data: "Hello" };
var pair: Pair<string, number> = { key: "age", value: 25 };
console.log("结果: " + JSON.stringify(result));
console.log("键值对: " + JSON.stringify(pair)); */
/* //定义数字字面量类型
let a :1|2|3|4|5;
a = 3;   //a只能赋值指定的几个值
type b = 1|2|3|4|5;    //类型别名
let bb :b = 2;
console.log(a);
console.log(bb); */
/* // 使用模板字面量类型定义事件名称
// `on${string}` 表示以 "on" 开头的任意字符串
type EventName = `on${string}`;
// 使用模板字面量类型定义处理器名称
// Capitalize 将字符串首字母大写
type Handler = `handle${Capitalize<string>}`;
// 使用模板字面量类型
var event: EventName = "onClick";
var handler: Handler = "handleSubmit";
console.log("事件: " + event);
console.log("处理器: " + handler);
// 错误：不符合模板格式
// var e: EventName = "click"; // 编译错误：不是以 "on" 开头 */
/* // `on${string}` 表示以 "on" 开头的任意字符串type a = `on${string}`;
let b:a="onClick";
console.log(b);
//let c :a = "afe"; //编译错误，不是以“on”开头 */
/* //抽象类
abstract class C{
  abstract show(name:string):string;
}
class Ca extends C{
  show(name:string):string
  {
    return name + "_1";
  }
}
class Cb extends C{
  show(name:string):string
  {
    return name + "_2";
  }
}
//多态类方法
function showC(c:C,name:string):void
{
  console.log(c.show(name));
}
let ca = new Ca();
let cb = new Cb();
showC(ca,"张三");
showC(cb,"李四"); */
/* // 定义一个类装饰器函数,类似于触发器，除此之外还有方法装饰器，访问器装饰器，属性装饰器，参数装饰器等
// 参数 target 就是被装饰的类构造函数
function sealed(target: Function) {
    // 打印装饰器被应用到的类名
    console.log("装饰器 applied to: " + target.name);
    // 使用 Object.seal 锁定构造函数和原型
    // 防止在运行时添加或删除属性
    Object.seal(target);
    Object.seal(target.prototype);
}
// 使用 @ 语法将装饰器应用到类上
@sealed
class Person {
    name: string;
    constructor(name: string) {
        this.name = name;
    }
}
// 创建实例测试
var person = new Person("RUNOOB");   //创建实例后触发类装饰器函数
console.log("创建: " + person.name);
// 尝试添加新属性（会被阻止，因为类被 seal 了）
// person.age = 25; // 静默失败 */
/* // 定义一个自定义类型守卫函数
// 返回类型使用 "value is Type" 格式
// 这告诉 TypeScript：当函数返回 true 时，参数类型就是 string
function isString(value: any): value is string {
    // 使用 typeof 检查是否为字符串
    return typeof value === "string";
}
// 另一个自定义守卫：检查是否为数字
function isNumber(value: any): value is number {
    return typeof value === "number";
}
// 定义一个数组类型守卫
function isArray(value: any): value is any[] {
    return Array.isArray(value);
}
// 处理值的函数
function processValue(value: string | number | any[]): void {
    // 使用自定义守卫进行类型检查
    if (isString(value)) {
        // TypeScript 知道 value 是 string 类型
        // 可以调用 toUpperCase() 方法
        console.log("字符串转大写: " + value.toUpperCase());
    } else if (isNumber(value)) {
        // TypeScript 知道 value 是 number 类型
        // 可以调用 toFixed() 方法
        console.log("数字格式化: " + value.toFixed(2));
    } else if (isArray(value)) {
        // TypeScript 知道 value 是数组类型
        console.log("数组长度: " + value.length);
    }
}
// 测试调用
processValue("hello");
processValue(3.14159);
processValue([1, 2, 3, 4, 5]); */
/* // 可能为 undefined 的字符串
function greet(name?: string): string {
    // 使用短路运算符：如果 name 为 undefined 或空字符串，使用默认值
    // 在 && 后的代码块中，TypeScript 知道 name 一定有值
    return name && "Hello, " + name;
}
// 测试
console.log(greet("RUNOOB"));
console.log(greet()); */
/* //可选链
class CC{
  name:string="";
  listA:number[]=[1,2,3,4,5,6]
  lost()
  {
    console.log("hello"+num);
  }
}
let c = new CC();
c.name = "张三";
console.log(c)
c?.lost();
console.log(c?.[10]); */
/* //Partial<T>可选属性,在一个类型的基础上变体为新类型就叫映射类型
// 定义用户接口，包含必填属性
interface User {
    // 用户 ID
    id: number;
    // 用户名
    name: string;
    // 用户邮箱
    email: string;
}
// Partial：将所有属性变为可选
// 转换后的类型所有属性都是可选的
type PartialUser = Partial<User>;
// 使用 PartialUser 类型
// 可以只提供部分属性，不需要全部提供
var user: PartialUser = { name: "Alice" };
console.log("部分用户: " + JSON.stringify(user));
user.id = 12;
console.log("后期添加接口属性: " + JSON.stringify(user)); */
/* //Required<T>必填属性
// 定义配置接口，属性都是可选的
interface Config {
    // 服务器地址
    host?: string;
    // 端口号
    port?: number;
}
// Required：将所有可选属性变为必填
// 转换后的类型所有属性都是必填的
type RequiredConfig = Required<Config>;
// 使用 RequiredConfig 类型
// 必须提供所有属性
var config: RequiredConfig = { host: "localhost", port: 8080 };
console.log("配置: " + JSON.stringify(config)); */
/* //Readonly<T>只读属性
// 定义用户接口
interface User {
    // 用户名
    name: string;
    // 用户年龄
    age: number;
}
// Readonly：将所有属性变为只读
// 转换后的类型所有属性都不能修改
type ReadonlyUser = Readonly<User>;
// 创建只读用户对象
var user: ReadonlyUser = { name: "Alice", age: 25 };
// 尝试修改只读属性会报错
// user.name = "Bob"; // 错误：只读属性不能修改
console.log("只读用户: " + JSON.stringify(user)); */
/* //Pack<U,V>选择属性，只实现U中选择的V属性
// 定义完整的用户接口
interface User {
    // 用户 ID
    id: number;
    // 用户名
    name: string;
    // 用户邮箱
    email: string;
    // 用户密码
    password: string;
}
// Pick：选择指定的属性组成新类型
// 从 User 中选择 id 和 name 属性
// 接口的其它属性不可用
type UserBasicInfo = Pick<User, "id" | "name">;
// 使用选择后的类型
var user: UserBasicInfo = { id: 1, name: "Alice" };
console.log("用户基本信息: " + JSON.stringify(user)); */
/* // Record<K, T> - 构造对象类型
// Record 构造一个对象类型，键的类型为 K，值的类型为 T。
// 常用于创建键值对映射、字典类型等。// 定义角色类型
type Role = "admin" | "user" | "guest";
// Record：构造对象类型
// 键为 Role 类型，值为字符串数组类型
type RolePermissions = Record<Role, string[]>;
// 使用 Record 创建权限映射
var permissions: RolePermissions = {
    // 管理员拥有所有权限
    admin: ["read", "write", "delete"],
    // 普通用户拥有读写权限
    user: ["read", "write"],
    // 访客只有读权限
    guest: ["read"]
};
console.log("管理员权限: " + permissions.admin);
console.log("访客权限: " + permissions.guest); */
/* //Exclude<T,U>排除类型，排除T中的U属性使其不可用
// 定义联合类型，包含 a、b、c、d
type T = "a" | "b" | "c" | "d";
// Exclude：从 T 中排除指定类型
// 排除 "a"、"b"、"c"，只保留 "d"
type NonABC = Exclude<T, "a" | "b" | "c">;
// 使用排除后的类型，只能赋值 "d"
var value: NonABC = "d";
console.log("值: " + value); */
/* //Extract<T,U>提取类型，提取T中的U属性可用
// 定义混合联合类型，包含字符串和数字
type T = "a" | "b" | "c" | 1 | 2 | 3;
// Extract：从 T 中提取指定类型
// 提取所有字符串类型："a"、"b"、"c"
type Letters = Extract<T, string>;
// 使用提取后的类型,T中的string属性可用
var letter: Letters = "a";
console.log("字母: " + letter); */
/* //NonNullable<T>排除空值 使空值不可用
// 定义混合类型，包含字符串、null、undefined、数字
type T = string | null | undefined | number;
// NonNullable：排除 null 和 undefined
// 转换后只保留 string 和 number
type NotNull = NonNullable<T>;
// 使用非空类型
var value: NotNull = "hello";
value = 42;
// 尝试赋值 null 会报错
// value = null; // 错误：不能赋值 null
console.log("值: " + value); */
/* // ReturnType<T>获取函数的返回类型
// 定义获取用户的函数
function getUser() {
    return { name: "Alice", age: 25 };
}
// 定义获取配置的函数
function getConfig() {
    return { host: "localhost", port: 8080 };
}
// ReturnType：获取函数的返回类型
// 提取 getUser 函数的返回类型
type UserType = ReturnType<typeof getUser>;
// 提取 getConfig 函数的返回类型
type ConfigType = ReturnType<typeof getConfig>;
// 使用提取的返回类型创建对象
var user: UserType = { name: "Bob", age: 30 };
var config: ConfigType = { host: "example.com", port: 3000 };
console.log("用户: " + JSON.stringify(user));
console.log("配置: " + JSON.stringify(config)); */
/* // 条件类型
// ToArray 会将类型 T 转换为数组类型
// 当 T 是联合类型时，会自动分布处理每个类型
type ToArray<T> = T extends any ? T[] : never;
// 联合类型会自动分布
// string | number 会分布为：ToArray<string> | ToArray<number>
// 即：string[] | number[]
type StrOrNum = ToArray<string | number>;
// 可以赋值 string[] 或 number[]
var arr: StrOrNum = ["hello","hi"];
// 也可以赋值 number
// arr = 42;
console.log("数组: " + arr); */
/* //泛型类型别名
type re<t> = {str:t}|{num:t};
let a:re<string> = {str:"hello"};
let b:re<number> = {num:134};
console.log(a);
console.log(b) */
/* //组合类型&
// 定义类型 A
interface A {
    a: string;
}
// 定义类型 B
interface B {
    b: number;
}
// 使用接口继承多个接口
// 需要使用 extends 继承多个接口
interface AB extends A, B {
    c: boolean;
}
// 使用交叉类型（更简洁）
// 直接使用 & 符号组合类型
type ABType = A & B & { c: boolean };
// 两种方式都能创建包含所有属性的类型
var obj: ABType = { a: "hello", b: 42, c: true };
console.log("对象: " + JSON.stringify(obj));
let ab :AB = {a:"aa",b:12,c:false};
console.log(ab); */
/* // 定义基础字符串字面量类型
type World = "world";
// 使用模板字面量类型
// `Hello ${World}` 相当于 "Hello world"
type Greeting = `Hello ${World}`;
// 只能赋值符合类型定义的字符串
var greeting: Greeting = "Hello world";
console.log("问候: " + greeting); */
/* // 带数字的模板类型
// ${number} 匹配任意数字
type Row = `row${number}`;
type Row10 = Row;  // row0, row1, row2... 直到 row9...
// 组合多个类型
type Variant = "primary" | "secondary";
type Size = "sm" | "md" | "lg";
// 这会生成 6 种组合：btn-primary-sm, btn-primary-md, btn-primary-lg...
type ClassName = `btn-${Variant}-${Size}`;
// 只能赋值生成的 6 种组合之一
var className: ClassName = "btn-primary-md";
console.log("类名: " + className); */
/* //定义类型的格式，模板
// 添加前缀的工具类型
// T 是任意字符串，P 是要添加的前缀
type Prefix<T extends string, P extends string> = `${P}${Capitalize<T>}`;
// 添加后缀的工具类型
// T 是任意字符串，S 是要添加的后缀
type Suffix<T extends string, S extends string> = `${Capitalize<T>}${S}`;
// 使用自定义工具类型
type HandlerName = Prefix<"click", "on">;
type ButtonId = Suffix<"submit", "Btn">;
var handler: HandlerName = "onClick";
var id: ButtonId = "SubmitBtn";
console.log("处理器: " + handler);
console.log("ID: " + id); */
/* // 从 Promise 中提取值类型
// 如果 T 是 Promise<V>，则返回 V；否则返回 never
type ValueOf<T> = T extends Promise<infer V> ? V : never;
// 测试
type Str = ValueOf<Promise<string>>;  // string
type Num = ValueOf<Promise<number>>; // number
type NotPrm = ValueOf<string>;        // never
// 使用示例
var promise: Promise<string> = Promise.resolve("hello");
var value: ValueOf<typeof promise> = "world";
console.log("字符串提取: " + (typeof value === "string" ? "成功" : "失败")); */
/* //联合类型，交叉类型
type a = {x:number,y:number,name:string,ty:string};
type b = {age:number,name:string,x:number};
type c = a | b;    //联合类型，包含a或b的所有属性
let c1:c = {x:1,name:"ja",y:56,ty:"egr"}
let c2:c = {age:12,name:"he",x:23,ty:"grg"}
console.log(c1);
console.log(c2);
type ab = a & b;   //交叉类型，a和b的属性都包含
let ab1:ab = {x:12,y:23,name:"hi",age:24,ty:"grt"};
console.log(ab1) */
/*//keyof
// 定义用户类型
interface User {
    id: number;       // 用户ID
    name: string;     // 用户名
    email: string;    // 邮箱
    age?: number;     // 年龄（可选）
}
// 使用 keyof 获取所有键的联合类型
// 结果: "id" | "name" | "email" | "age"
type UserKeys = keyof User;
// 测试 keyof
function getProperty<T, K extends keyof T>(obj: T, key: K): T[K] {
    return obj[key];
}
const user: User = {
    id: 1,
    name: "Alice",
    email: "alice@example.com"
};
// 获取 name 属性
const userName: string = getProperty(user, "name");
console.log("用户名: " + userName); */
/* //keyof
// 定义用户类型
interface User {
    id: number;
    name: string;
    email: string;
}
// 使用索引访问类型获取属性类型
type UserId = User["id"];      // number
type UserName = User["name"];  // string
// 可以使用联合类型进行多个键的访问
type UserIdAndName = User["id" | "name"];  // number | string
// 使用 keyof 获取所有属性类型的联合
type AllUserValues = User[keyof User];  // number | string
// 实际使用示例
function getValue<T, K extends keyof T>(obj: T, key: K): T[K] {
    return obj[key];
}
const user: User = { id: 1, name: "Bob", email: "bob@test.com" };
// 获取 id 类型
const idValue: number = getValue(user, "id");
console.log("ID: " + idValue);
// 获取 name 类型
const nameValue: string = getValue(user, "name");
console.log("Name: " + nameValue); */
/* // 定义配置类型
interface Config {
    apiUrl: string;
    timeout: number;
    retry: boolean;
}
// 只能获取存在的键,K只能在T中
function getConfigValue<T, K extends keyof T>(
    config: T,
    key: K
): T[K] {
    return config[key];
}
// 定义配置
const config: Config = {
    apiUrl: "https://api.example.com",
    timeout: 5000,
    retry: true
};
// 正确：键存在
const url: string = getConfigValue(config, "apiUrl");
const timeoutVal: number = getConfigValue(config, "timeout");
// 错误：键不存在（TypeScript 会报错）
// const invalid = getConfigValue(config, "unknown");
console.log("API URL: " + url);
console.log("Timeout: " + timeoutVal); */
/* // 定义用户类型
interface User {
    id: number;
    name: string;
    email: string;
    age: number;
}
// 将所有属性变为可选,映射类型
type PartialUser = Partial<User>;
// 将所有属性变为只读
type ReadonlyUser = Readonly<User>;
// 自定义映射类型：将所有属性变为可选且字符串化
// 映射类型使用keyof要在前面加in关键字
type Stringify<T> = {
    [P in keyof T]: string;
};
type StringifiedUser = Stringify<User>;
// 实际使用示例
const partialUser: PartialUser = {
    id: 1,
    name: "Alice"
    // email 和 age 可选
};
const readonlyUser: ReadonlyUser = {
    id: 1,
    name: "Bob",
    email: "bob@test.com",
    age: 25
};
// readonlyUser.name = "Charlie"; // 错误：只读
console.log("部分用户: " + partialUser.name);
console.log("只读用户: " + readonlyUser.name); */
// 协变：子类赋值给父类，类型安全
// 逆变：父类赋值给子类，类型不安全
/* // 接口：适合定义对象类型，支持声明合并
interface User {
    id: number;
    name: string;
}
// 扩展接口
interface User {
    email: string;
}
// 类型别名：适合联合类型、元组、函数类型
type ID = string | number;
type Status = "pending" | "success" | "error";
type Callback = (data: string) => void;
// 工具类型通常使用 type
type PartialUser = Partial<User>;
type ReadonlyUser = Readonly<User>;
// 性能考虑：接口的编译速度通常比类型别名快
// 对于简单的对象类型，可以使用 interface
interface Point {
    x: number;
    y: number;
}
// 对于联合类型，使用 type
type Shape = Circle | Square | Triangle; */
function gets() {
    let a = [];
    for (let i = 0; i < 10; i++) {
        a.push(i);
    }
    return a;
}
console.time("运行时间");
let a = gets();
console.log(a);
console.timeEnd("运行时间");
