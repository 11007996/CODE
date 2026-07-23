tsParticles.load("tsparticles", {
    fpsLimit: 60,
    //粒子
    particles: {
        number: {
            value: 8,
            density: {//密度
                enable: true,
                value_area: 100
            }
        },
        color: {
            value: "#ff0000",
            animation: {//颜色变化动画
                enable: true,
                speed: 20,
                sync: true
            }
        },
        shape: {//形状
            type: "circle",
            stroke: {
                width: 0,
                color: "#000000"
            },
            polygon: {//多边形
                nb_sides: 5
            },
            image: {
                width: 100,
                height: 100
            }
        },
        opacity: {//透明度
            value: 0.3,
            random: false,
            anim: {//透明度变化动画
                enable: false,
                speed: 0.1,
                opacity_min: 0.1,
                sync: false
            }
        },
        size: {//大小
            value: 3,
            random: true,
            anim: {//大小变化动画
                enable: false,
                speed: 0.1,
                size_min: 0.5,
                sync: false
            }
        },
        line_linked: {//连接线
            enable: true,
            distance: 100,
            color: "random",
            opacity: 0.2,//0.4
            width: 1,
            triangles: {//三角形
                enable: true,
                color: "#ffffff",
                opacity: 0.01
            }
        },
        move: {//移动
            enable: true,
            speed: 1.5,//6
            direction: "none",
            random: false,
            straight: false,
            out_mode: "out",
            attract: {//吸引
                enable: false,
                rotateX: 600,
                rotateY: 1200
            }
        }
    },
    //交互性
    interactivity: {
        detect_on: "canvas",
        events: {
            onhover: {
                enable: true,
                mode: "repulse"
            },
            onclick: {
                enable: true,
                mode: "push"
            },
            resize: true
        },
        modes: {
            grab: {
                distance: 400,
                line_linked: {
                    opacity: 1//1
                }
            },
            bubble: {
                distance: 400,
                size: 40,
                duration: 2,
                opacity: 0.8,//0.8
                speed: 3
            },
            repulse: {
                distance: 200
            },
            push: {
                particles_nb: 4
            },
            remove: {
                particles_nb: 2
            }
        }
    },
    retina_detect: true,
    background: {//背景
        color: "transparent",
        image: "",
        position: "50% 50%",
        repeat: "no-repeat",
        size: "cover"
    }
});