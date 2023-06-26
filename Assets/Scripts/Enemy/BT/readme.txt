OnStart和OnEnd分别在事件开始前，和事件结束后调用，
如果是延迟事件，会等返回成功或失败后再调用OnEnd，
返回Running的时候是不会调用它的

对于第一次死亡以及进入第二阶段的思路:
1.isDie(Conditional): 判断敌人是否死亡
2.DeathNoBlood(Action): 播放敌人第一次死亡动画及逻辑，并有isDeathNoBlood(SharedBool)告诉isDeathNoBlood = true;

(在此插入一个isDie(Conditional): )
3.isDeathNoBlood(Conditional): 
4.Death(Action): 