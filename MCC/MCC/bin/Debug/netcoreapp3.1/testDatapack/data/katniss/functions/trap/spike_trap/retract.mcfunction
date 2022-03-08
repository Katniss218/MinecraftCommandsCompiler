tp @s ~ ~-0.1 ~
scoreboard players remove @s npc_ai1 1
execute unless score @s npc_ai2 matches 1 run playsound minecraft:block.iron_trapdoor.open hostile @a ~ ~ ~ 1 0.4
execute unless score @s npc_ai2 matches 1 run scoreboard players set @s npc_ai2 1
scoreboard players set @s npc_ai3 0