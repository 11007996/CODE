
// 设定参数类型
/* function greet(person: string, date: Date) {
  console.log(`Hello ${person}, today is ${date.toDateString()}!`);
}
 
greet("Maddison", new Date()); 
*/

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





