# Mount & Blade 2D (Unity 6)

This repository contains a Unity 6 project setup and design notes for building a 2D take on Mount & Blade. It focuses on fast iteration: core gameplay loops (roaming, recruiting, battling) are broken into small systems that can be tested in isolation.

## Vision
- Side-scrolling overworld where the player leads a party across a map of settlements and roaming bands.
- Real-time melee combat with directional attacks, blocks, and limited stamina.
- A lightweight simulation layer for parties, morale, and recruitment.
- Highly modular data-driven content (ScriptableObjects for weapons, troops, factions).

## Project layout
- `Assets/Scripts/Core` — time/clock, event bus, common data structures.
- `Assets/Scripts/Character` — character stats, health, stamina, experience.
- `Assets/Scripts/Party` — party composition, morale, recruiting and wages.
- `Assets/Scripts/Combat` — combat state machine, hit detection, damage resolution.
- `Assets/Scripts/World` — overworld controller, map nodes (towns, camps), roaming AI.
- `Assets/Scripts/UI` — HUD, interaction prompts, recruitment panels.
- `Assets/ScriptableObjects` — data assets for troops, weapons, factions.
- `Docs` — design documentation and task lists.

## Getting started (Unity 6)
1. Create a **2D (URP)** project in Unity 6 and point it at this repository folder.
2. Import the new Input System package and enable it in **Project Settings → Player**.
3. Add the following layers: `Hitbox`, `Hurtbox`, `Terrain`, `Interaction`.
4. Create URP renderer features for 2D lighting if you plan to use day/night cues.
5. Add an empty scene `Scenes/Overworld` with:
   - A `Grid` with `Tilemap` for terrain.
   - A `Player` prefab with `Rigidbody2D`, `Collider2D`, `OverworldController`, and optional `OverworldInputReader` (wire an Input Action named `Move`).
   - A `GameManager` prefab with `GameClock` and `EventBus` components.
6. Add a `PartyManager` prefab with `PartyRoster` + `MoraleSystem`; wire UI to `RecruitmentPanel`.
7. For combat sandboxes, create a scene with two fighters each having `CombatStateMachine`, `CombatInputController`, `HealthComponent`, `StaminaComponent`, and `HitResolver` paired with `Hitbox` on weapon hitboxes (and `Hurtbox` on the body).

## Immediate milestones
- Overworld movement prototype with camera follow and nav boundaries.
- Basic combat arena scene with two units, directional attack/block, and stamina drain.
- Recruitment panel in towns that pulls data from ScriptableObjects and updates the party.
- Save/load for party roster and resources using JSON.

## Contributing
- Keep systems modular; favor interfaces and ScriptableObjects for content.
- Use deterministic updates in the overworld (ticks) and fixed updates for combat physics.
- Keep data assets small and composable—avoid hardcoding stats in scripts.

## Testing
- Play mode test scenes should live under `Assets/Tests/PlayMode`.
- Use `UnityEngine.TestTools` for simulation-heavy systems (party morale, wages).
- Prefer per-system validation scenes (e.g., `Scenes/Tests/CombatSandbox`).
