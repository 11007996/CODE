# 一级标题

## 二级标题

###### 六级标题

末尾两个空格表示回车，相当于<br>

 文本**加粗**

文本__加粗__

文本*斜体*

文本_斜体_

文本***加粗斜体***

文本___加粗斜体___

~~删除线~~



> 引用
>
> > 嵌套引用
> >
> > - 嵌套引用

1. 有序列表
2. 有序列表
   1. 有序列表
   2. 有序列表

- 无序列表
- 无序列表

* 无序列表
* 无序列表

+ 无序列表
+ 无序列表
  + 无序嵌套列表

`<p>代码行<\p>`

```html
<p>代码段<\p>
<p>代码段<\p>
<p>代码段<\p>
```

---

***

+++

这是一个[链接](https://markdown.com.cn/basic-syntax/links.html)

这是一个**[链接](https://markdown.com.cn/basic-syntax/links.html)**

这是一个[`链接`](https://markdown.com.cn/basic-syntax/links.html)

这是一个[链接](https://markdown.com.cn/basic-syntax/links.html "链接提示")

<https://markdown.com.cn/basic-syntax/links.html>

[引用链接][1]: https://markdown.com.cn/basic-syntax/links.html
[引用链接][1]: <https://markdown.com.cn/basic-syntax/links.html>
[引用链接][1]: <https://markdown.com.cn/basic-syntax/links.html>

[页内链接](#六级标题)



This is  [引用链接][1]  reference-style link. 
[1]: <https://markdown.com.cn/basic-syntax/links.html>



![图片](D:\zzzzzzzz\杂资料\图片收集\Git常用命令.jpg '图片提示')

[![图片加链接](D:\zzzzzzzz\杂资料\图片收集\Git常用命令.jpg "Shiprock")](https://markdown.com.cn)



\* 星号转义

\> 转义符

H<sub>2</sub>O
H<sup>2</sup>O


<p>内嵌套HTML语句</p>
<table><tr>内嵌套HTML语句</tr><br><tr>内嵌套HTML语句</tr></table>


引用注脚[^1]
[^1]: Here is the footnote.

[引用链接]: 



| 表格 | 表格 | 表格   |
| :-----: | ------: | :------ |
| DSGR  |        |        |
| S     | SGRESR | SERHEH |
|       |        |        |

- [x] 复选框
- [ ] 复选框

![图片描述][2] 

[2]: <D:\zzzzzzzz\杂资料\图片收集\Git常用命令.jpg> "图片标题"



脚注[^1]

[1]: 这里是脚注

<ins>下划线文字</ins>

<font color="red">红色文字</font>

<p style="color:blue">蓝色文本</p>

这是可见的段落。
[这是一个注释]: #
这是另一个可见的段落。

 :warning: **警告:** 这是警告提示。
 :memo: **注意:** 这是注意提示。
 :bulb: **技巧:** 这是技巧提示。





# LaTeX 数学公式和符号表

#### 1.上标和下标

$$
上标：  X^2+Y^3 = Z
$$

$$
下标：  X_2+Y_3 = Z
$$

#### 2.常用括号

$$
(X+Y)
[X+Y]
\{X+Y\}
|X+Y|
\langle X+Y \rangle
\|X+Y\|
$$

#### 3.放大括号
$$
( \big( \Big( \bigg( \Bigg(
$$

$$
\{ \big\{ \Big\{ \bigg\{ \Bigg\{
$$

#### 4.常用矩阵

$$
\begin{matrix}
1 & 2 & 3\\
a & b & c
\end{matrix}  \\
$$
$$
\begin{pmatrix}
1 & 2 & 3\\
a & b & c
\end{pmatrix}  \\
$$
$$
\begin{bmatrix}
1 & 2 & 3\\
a & b & c
\end{bmatrix}  \\
$$
$$
\begin{Bmatrix}
1 & 2 & 3\\
a & b & c
\end{Bmatrix}   \\
$$
$$
\begin{vmatrix}
1 & 2 & 3\\
a & b & c
\end{vmatrix}  \\
$$
$$
\begin{Vmatrix}
1 & 2 & 3\\
a & b & c
\end{Vmatrix}  \\
$$
$$
\begin{smallmatrix}
1 & 2 & 3\\
a & b & c
\end{smallmatrix}
$$
$$
f(x) = \begin{cases}
x^2 & \text{if } x \geq 0 \\
-x^2 & \text{if } x < 0
\end{cases}
$$
$$
f(x) = \begin{cases}
x^2 & (x \geq 0) \\
-x^2 & (x < 0)
\end{cases}
$$
$$
E = mc^2 \tag{1}
$$



#### 5.分式和二项式

$$
\frac{x}{y}
$$

$$
\frac{x+1}{y+\frac{1}{2}}
$$

$$
\binom{n}{k}
$$

#### 6.对齐
展示长公式
$$
\begin{multline*} + 公示内容 + 中间用 \\ 分行 + 用 \end{multline*}
$$
拆分、对齐方程
$$
\begin{align*} + 公示内容 + 中间用 \\ 分行 + 方程等号前加上 & 用 \end{align*}
$$
居中显示方程（不以等号对齐）
$$
\begin{gather*} + 公示内容 + 中间用 \\ 分行 + 方程等号前加上 & 用 \end{gather*}
$$

#### 7.运算符(三角函数、对数、极限等)

$$
\cos \\ \tan \\ \csc \\ \lim \\ \exp \\ \dim \\ \min \\ \max \\ \arcsin \\ \sin \\ \lg \\ \tanh \\ \ln \\ \arg \\ \log \\ \ker \\ \limsup \\ \Pr \\ \hom \\ \liminf
$$

#### 8.空格

$$
aaa \quad bbb \\
aaa \, bbb \\
aaa \: bbb \\
aaa\;bbb \\
aaa \! bbb \\
aaabbb \\
aaa \qquad bbb
$$

#### 9.积分、累加、连乘、极限符号

$$
1.\quad Integral (\int_{a}^{b}x^2 \,dx) inside \, text  \\
2.\quad [\int_{a}^{b}x2\,dx]  \\
3.\quad [\oint_V f(s)\,ds]  \\
4.\quad \iint_D f(x,y) \, dx \, dy  \\
5.\quad \iiint_V f(x,y,z) \, dx \, dy \, dz  \\
6.\quad \lim_{n \to \infty} \left(1 + \frac{1}{n}\right)^n = e  \\
7.\quad \lim_{x \to 0^+} \frac{1}{x} = +\infty
$$

#### 10.累加与连乘

$$
Sum \sum_{n=1}^{\infty} 2^{-n} =1   \\
\sum_{n=1}^{\infty}2^{-n}=1    \\
Product \prod_{i=a}^{b} f(i)     \\
\prod_{i=a}^{b}f(i)
$$

#### 11.极限

$$
Limit \lim_{x\to\infty} f(x)     \\
\lim_{x\to\infty}f(x)
$$

#### 12.希腊字母表

$$
1.\quad \alpha A    \\
2.\quad \beta B    \\
3.\quad \gamma \Gamma  \\
4.\quad \delta \Delta   \\
5.\quad \epsilon \varepsilon E \\
6.\quad \zeta Z  \\
7.\quad \eta  H   \\
8.\quad \theta  \vartheta \Theta  \\
9.\quad \iota I   \\
10.\quad \kappa K   \\
11.\quad \lambda \Lambda  \\
12.\quad \mu M   \\
13.\quad \nu N   \\
14.\quad \xi \Xi  \\
15.\quad o O \\
16.\quad \pi \Pi   \\
17.\quad \rho \varrho P   \\
18.\quad \sigma \Sigma  \\
19.\quad \tau T  \\
20.\quad \upsilon \Upsilon  \\
21.\quad \phi \varphi \Phi  \\
22.\quad \chi X   \\
23.\quad \psi \Psi  \\
24.\quad \omega \Omega
$$

#### 13.各种箭头、字母上方的标记

$$
1.\quad \leftarrow  \\
2.\quad \Leftarrow  \\
3.\quad \rightarrow  \\
4.\quad \Rightarrow  \\
5.\quad \leftrightarrow  \\
6.\quad \rightleftharpoons  \\
7.\quad \uparrow  \\
8.\quad \downarrow \\
9.\quad \Uparrow  \\
10.\quad \Downarrow  \\
11.\quad \Leftrightarrow  \\
12.\quad \Updownarrow  \\
13.\quad \mapsto  \\
14.\quad \longmapsto  \\
15.\quad \nearrow  \\
16.\quad \searrow  \\
17.\quad \swarrow  \\
18.\quad \nwarrow  \\
19.\quad \leftharpoonup  \\
20.\quad \rightharpoonup  \\
21.\quad \leftharpoondown  \\
22.\quad \rightharpoondown  \\
23.\quad \hat X, \widehat{X+Y}  \\
24.\quad \overline X, \underline X  \\
25.\quad \dot{X},\ddot{X}  \\
26.\quad \tilde{X}, \widetilde{X}
$$

#### 14.四则运算、关系符

$$
1.\quad \times  \\
2.\quad \cdot  \\
3.\quad \div  \\
4.\quad \cap  \\
5.\quad \cup  \\
6.\quad \neq  \\
7.\quad \leq  \\
8.\quad \geq  \\
9.\quad \in  \\
10.\quad \perp  \\
11.\quad \notin  \\
12.\quad \subset  \\
13.\quad \simeq  \\
14.\quad \approx  \\
15.\quad \wedge  \\
16.\quad \vee  \\
17.\quad \oplus  \\
18.\quad \otimes  \\
19.\quad \Box  \\
20.\quad \boxtimes  \\
21.\quad \equiv  \\
22.\quad \cong  \\
23.\quad \pm  \\
24.\quad \mp  \\
25.\quad \sqrt{2}  \\
26.\quad \odot \\
27.\quad \circ  \\
28.\quad \approxeq  \\
29.\quad \approx  \\
30.\quad \simeq  \\
31.\quad \asymp
$$

#### 15.其它符号

$$
1.\quad \infty  \\
2.\quad \forall  \\
3.\quad \Re  \\
4.\quad \Im  \\
5.\quad \nabla  \\
6.\quad \exists  \\
7.\quad \partial  \\
8.\quad \nexists  \\
9.\quad \emptyset  \\
10.\quad \varnothing  \\
11.\quad \wp  \\
12.\quad \complement  \\
13.\quad \neg  \\
14.\quad \cdots  \\
15.\quad \square  \\
16.\quad \surd  \\
17.\quad \blacksquare  \\
18.\quad \triangle
$$
#### 16.逻辑运算

$$
\land
\lor
\lnot
\implies
\iff
$$



#### 17.字体

$$
\textrm{Apple}, \textsf{Apple},\texttt{Apple},\mathbb{Apple},\mathcal{Apple}
$$

#### 18.向量
$$
\vec{v}, \quad \overrightarrow{AB}
$$

#### 19.求导
$$
1.\quad \frac{\mathrm{d}y}{\mathrm{d}x}  \\
2.\quad \frac{\partial f}{\partial x}  \\
3.\quad \frac{\partial ^{n} f}{\partial x^{n}}  \\
4.\quad \frac{\mathrm{d}^{n} y }{\mathrm{d} x^{n}}  \\
5.\quad \frac{ y^{'} }{ x^{'} }  \\
6.\quad \frac{ \dot y }{ \dot x }  \\
7.\quad \frac{ \ddot y }{ \ddot x }  \\
8.\quad \frac{ \dddot y }{ \dddot x }  \\
9.\quad \nabla f
$$





