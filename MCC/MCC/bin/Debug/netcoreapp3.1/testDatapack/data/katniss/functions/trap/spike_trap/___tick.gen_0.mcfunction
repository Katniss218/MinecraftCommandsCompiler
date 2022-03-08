execute if score @s npc_ai1 matches ..9 positioned ~ ~1 ~ if entity @a[distance=..1.5] positioned ~ ~-1 ~ run function katniss:trap/spike_trap/extend
execute if score @s npc_ai1 matches 3..10 positioned ~ ~1 ~ if entity @a[distance=..1.5] positioned ~ ~-1 ~ run
execute if score @s npc_ai1 matches 1..10 positioned ~ ~1 ~ unless entity @a[distance=..1.5] positioned ~ ~-1 ~ run function katniss:trap/spike_trap/retract
execute if score @s npc_ai1 matches 0 if score @s npc_ai2 matches 1 run scoreboard players set @s npc_ai2 0
execute if score @s npc_ai1 matches 1 positioned ~ ~1 ~ if entity @a[distance=..1.5] run playsound minecraft:block.iron_trapdoor.open hostile @a ~ ~ ~ 1 0.1