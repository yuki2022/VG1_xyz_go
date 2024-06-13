# XYZ, Go! - Game Introduction and Documentation

## Game Demo and Scope

"XYZ, Go!" is a thrilling action-adventure game designed for our CSE450 demo1, available on itch.io. The game showcases a fully playable core loop, featuring engaging mechanics and clear goals. It seamlessly integrates advanced game design techniques, such as feedback loops and flow, providing players with an immersive and satisfying experience. The project scope meets and exceeds expectations, presenting a polished and comprehensive game demo.

## Control Guide

- **K**: Open the menu
- **A, D**: Move left and right
- **Space**: Jump (double jump available)
- **LMB**: Shoot fireballs (basic attack, deals 1 damage)
- **Q**: Shoot ice cones (costs 1 mana, deals 1 damage and slows enemies by 50%)
- **W**: Release a toxic cloud (costs 3 mana, deals 1 damage per second to all enemies it touches)
- **E**: Summon Nocturne (costs 3 mana, has 2 health, blocks enemy projectiles, and perishes with the enemy it hits)
- **R**: Summon 3 Nocturnes at once (requires a Nocturne permission ticket)

## Gameplay Objectives

In "XYZ, Go!", players can pursue various goals:

- Reach the door in the castle to embark on new adventures in unreleased levels.
- Achieve higher and higher levels.
- Live a happy life with the little princess waiting at the endpoint.

Players receive damage from touching enemies or enemy projectiles. Falling off the edge restarts the game at the nearest checkpoint. Killing enemies grants EXP points, allowing players to level up, restore health, and gain more mana. Players start the game by moving to the right.

## Craft System

Dead enemies generate materials that players can collect. Players can enter the Workshop by going through the door on the left side of the start point. In the Workshop, materials collected from defeated enemies can be dropped into a pot and refined to create props. These props aid players in defeating more enemies.

### Props:

1. **Health Bottle** (press 1 to use): Restores 1 health.
2. **Mana Bottle** (press 2 to use): Restores 1 mana.
3. **EXP Bottle** (press 3 to use): Grants 10 EXP points.
4. **Purple Crystal**: Decreases the mana cost of the toxic cloud by 1 until death.
5. **Blue Crystal**: Decreases the mana cost of the ice cone (unlimited use) until death.
6. **Nocturne Permission Ticket**: Consumable that allows pressing R to summon 3 Nocturnes.
7. **Scroll**: Increases ability power by 1.
8. **Sword**: "Kill steal" sword that eliminates enemies with 1 health and runs away.

## Detailed Contributions

### Xiaohai

- Introduced skillsets, crafting system, and level-up mechanisms.
- Designed and implemented the Workshop scene and the player prefab, ensuring separation of functions to avoid integration issues.

### Chongtian

- Developed enemy behaviors and traps.
- Implemented the health bar system, camera work, and level design elements such as moving platforms and water terrain.

### Yucen

- Coded player movements and jumps.
- Implemented obstacles and collision events.
- Designed projectiles and their collision effects.
- Created the main menu, level menu, and functional buttons.
- Developed animations for fireballs and toxic clouds.

### Ruowen

- Designed prefabs and layout elements, ensuring smooth player progression.
- Developed the player controller with smooth movement and shooting mechanisms.
- Managed sound effects for various game actions.
- Created the level and expense counter in the UI, motivating players to level up.

## Team Postmortem

### Positives

**Ruowen:**

- Weekly feedback from peers and professors helped identify and address issues, optimizing the game for a better player experience.

### Negatives

**Chongtian:**

- Merge conflicts on GitHub due to multiple team members working on the same parts simultaneously, requiring extra time to resolve.

### Recommendations

**Yucen:**

- Assign specific days for team members to work on the game, preventing conflicts and ensuring smoother collaboration.

**Xiaohai:**

- Use independent controllers for new scenes or features and ensure thorough playtesting before uploading to avoid integration issues.

## Postmortem

### Successes

**Chongtian:**

- Completed the core game loop with interesting mechanics and smooth character movements.
- Integrated multiple attack methods and pathways to achieve goals.

### Failures

**Xiaohai:**

- Due to time constraints, focused on learning interesting mechanics rather than redundant work, resulting in limited levels and enemy types. Lacked a boss fight and comprehensive sound and background integration for the workshop scene. Some visual elements had undesired white spaces due to the absence of dedicated artists.

### Lessons Learned

**Ruowen:**

- Gained valuable skills in designing players, enemies, and projectiles through tutorials and collaborative idea-sharing.

**Yucen:**

- Utilized imagination and creativity to design a unique game, continuously optimizing it based on feedback, demonstrating the importance of iterative improvement and player-centric design.

"XYZ, Go!" is an ambitious project that showcases our collective skills and dedication, offering an engaging and challenging gaming experience. We look forward to further developing and refining the game, incorporating player feedback to create an even more immersive adventure.

