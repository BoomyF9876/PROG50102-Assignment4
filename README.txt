Unity version 6000.0.60f1

URP note: None

Controls: WASD to move character, Left mouse click to shoot

Architecture:
BotController: PlayerController.cs, EnemyController.cs
Refactor: Added ShootingController.cs, CapsuleCastCollision.cs, RayCastCollision.cs
Line-of-sight: The Gizmo method for player, Raycast for enemies
State-Driven Camera: idle camera, walking camera, victory camera

Asset credits:
-- Castle Guard 01 https://www.mixamo.com/#/?page=1&query=castle+guard+01&type=Character
-- Happy Idle https://www.mixamo.com/#/?page=1&query=happy+idle&type=Motion%2CMotionPack
-- Catwalk walk https://www.mixamo.com/#/?page=1&query=catwalk+walk&type=Motion%2CMotionPack
-- Drunk run forward https://www.mixamo.com/#/?page=1&query=drunk+run+forward&type=Motion%2CMotionPack
-- Gunplay https://www.mixamo.com/#/?page=1&query=gunplay&type=Motion%2CMotionPack
-- Victory Idle https://www.mixamo.com/#/?page=1&query=victory+idle&type=Motion%2CMotionPack
-- Crypto https://www.mixamo.com/#/?page=1&query=crypto&type=Character
-- Old Man Idle https://www.mixamo.com/#/?page=1&query=old+man+idle&type=Motion%2CMotionPack
-- Walking https://www.mixamo.com/#/?page=2&query=walking&type=Motion%2CMotionPack
-- SWAV_6.wav, SWAV_9.wav https://sounds.spriters-resource.com/ds_dsi/kerorpg/asset/394815/

Known issues:
-- Moving controller logic is not intuitive. WASD is mapped to relative direction not absolute direction.
-- Only one type of obstacle
