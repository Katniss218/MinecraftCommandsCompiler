tp @s ~ ~ ~
execute at @e[type=zombie] run summon zombie ~ ~ ~
scoreboard players add __global__ __utils__ 1
execute as @s runfunction katniss:utilities/spawn_enemy.gen_0