#include <iostream>
#include <cmath>
struct polar
{
    double distance;
    double angle;
};
struct rect
{
    double x;
    double y;
};

polar rect_to_polar(rect dapos);
void show_polar(polar dapos);
int main()
{
    using namespace std;
    rect rplace;
    polar pplace;

    cout << "Enter the x and y value: ";
    while(cin >> rplace.x >> rplace.y)    //分别输入两个值
    {
        pplace = rect_to_polar(rplace);
        show_polar(pplace);
        cout << "Next two numbers (q to quit): ";
    }
    cout << "Done.\n";
    return 0;
}

polar rect_to_polar(rect xypos)
{
    using namespace std;
    polar answer;
    answer.distance =sqrt(xypos.x*xypos.x + xypos.y*xypos.y);  //开方
    answer.angle = atan2(xypos.y,xypos.x);   //求两点间角度
    return answer;
}

void show_polar(polar dapos)
{
    using namespace std;
    const double Rad_to_deg = 57.2957;   // 180/PI
    cout << "distance: " << dapos.distance;
    cout << ", angle = " << dapos.angle*Rad_to_deg;   //弧度转角度
    cout << " degrees\n";
}

