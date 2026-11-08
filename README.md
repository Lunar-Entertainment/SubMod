# Subnautica: Call of the Abyss 🌊🦑

[![Build Subnautica Mod](https://github.com/Lunar-Entertainment/SubMod/actions/workflows/build.yml/badge.svg)](https://github.com/Lunar-Entertainment/SubMod/actions/workflows/build.yml)
[![Subnautica 2.0](https://img.shields.io/badge/Subnautica-Living%20Large%20(2.0+)-blue.svg)](https://unknownworlds.com/subnautica/)
[![Framework](https://img.shields.io/badge/Framework-Nautilus%201.0-brightgreen.svg)](https://github.com/SubnauticaModding/Nautilus)
[![BepInEx](https://img.shields.io/badge/BepInEx-5.4.21-orange.svg)](https://github.com/BepInEx/BepInEx)

**Call of the Abyss** is an expansive expansion mod for Subnautica 1 (Living Large / 2.0+ update). It introduces a terrifying deep ocean trench biome, high-performance submersible systems, base buildables, apex predator enemies, exotic items, and survival mechanics including deep hypothermia and barometric pressure crushing hazards.

---

## 🌟 Features Overview

### 1. 🌌 New Biome: The Abyssal Trench
- **Deep Midnight Atmosphere**: Plunges deep beneath the Grand Reef and dunes into pitch-black waters (`-1350, -850, 420`) where sunlight cannot penetrate.
- **Atmospheric Fog & Lighting**: High light absorption and dense scattering volume with cold bioluminescent illumination.
- **Audio Ambience**: Custom deep-sea acoustics and eerie FMOD background audio tracks.
- **Fast Travel**: Accessible in console via `goto abyssaltrench` or `biome abyssaltrench`.

---

### 2. ⚡ Vehicles & Modules
- **Void Depth Module MK4**:
  - Pushes vehicle operational limits, extending maximum crush depth to **3,500 meters**!
  - Equipped in Moonpool Modification / Upgrade consoles.
- **Quantum Overdrive Module**:
  - Interactive chargeable boost module for the Seamoth.
  - Channels plasma bursts into propulsion thrusters for a 250% forward velocity impulse!
- **Hydro-Thermal Pulse Shield**:
  - Active defensive barrier that discharges thermal-kinetic shockwaves within 30m, repelling aggressive trench predators and leviathans.

---

### 3. 🏗️ Base Building & Constructibles
- **Abyssal Geothermal Core**:
  - Heavy seabase reactor generating up to 500W of continuous power by converting thermal gradients in deep hydrothermal areas.
- **Abyssal Desalination Station**:
  - High-pressure reverse-osmosis filtration wall console. Purifies deep-sea oceanic brine into Large Filtered Water and Salt crystals at accelerated speeds.
- **Abyssal Titanium Locker**:
  - Ultra-high capacity reinforced storage locker featuring an **8x10 (80 slots)** inventory containment grid.

---

### 4. 🦈 Enemies & Fauna
- **The Abyssal Phantom**:
  - Lethal apex predator roaming total darkness using dorsal bioluminescent lures and echolocation.
  - Features 750 HP, high swim speed, and devastating bite attacks (45 damage) that target divers and submersibles.
  - Drops **Abyssal Phantom Teeth** during violent collisions and combat.
- **Abyssal Phantom Egg**:
  - Rare creature egg incubated in Alien Containment Units (requires 2 ACU stacks). Hatches a baby Abyssal Phantom.

---

### 5. 💎 New Items & Equipment
- **Abyssal Crystal**:
  - Naturally glowing cyan mineral found in the deepest corridors of the Grand Reef, Lost River, and Abyssal Trench.
- **Quantum Alloy Ingot**:
  - Super-alloy forged from Plasteel, Abyssal Crystals, Diamond, and Lead.
- **Abyssal Thermal Dive Suit**:
  - State-of-the-art diving suit insulated with aerogel and quantum micro-composites. Shields the player from sub-zero ocean temperatures and barometric pressure down to 3,500m.
- **Abyssal Kinetic Harpoon**:
  - Upgraded stasis rifle firing dense electromagnetic kinetic pulses to disable hostile trench predators.
- **Abyssal Luminescent Flare**:
  - Chemical torch burning for over 30 minutes with high-lumen illumination that pierces abyssal fog.

---

### 6. ❄️ Survival Mechanics
- **Deep-Sea Hypothermia**:
  - Swimming in waters below 450m or inside the Abyssal Trench causes body temperature to decay from 37°C.
  - Sub-28°C causes shivering camera oscillations and audio-visual alerts.
  - Sub-20°C induces hypothermic shock damage.
  - Staying inside a submarine, base, or equipping the **Abyssal Thermal Dive Suit** preserves body temperature.
- **Hyperventilation Oxygen Drain**:
  - Oxygen consumption rate is **doubled** when suffering from hypothermic shock.
- **Barometric Pressure Crushing**:
  - Diving past 1,200m without vehicle hull shelter or the Abyssal Thermal Dive Suit causes continuous barometric crush damage.

---

### 7. ⚙️ In-Game Configuration Menu
Configurable directly from the **Options > Mods > Call of the Abyss** pause menu powered by Nautilus:
- `Enable Deep Trench Hypothermia`: Toggle temperature mechanics.
- `Enable Barometric Pressure Hazard`: Toggle 1,200m+ crush depth damage.
- `Phantom Predator Aggression`: Scale predator damage and aggression multiplier (0.5x – 3.0x).
- `Quantum Propulsion Overdrive Boost`: Adjust booster velocity multiplier (1.5x – 5.0x).
- `Atmospheric Abyssal Darkness`: Toggle trench fog and light scattering density.

---

## 🛠️ Installation Guide

1. Install **[BepInEx Pack for Subnautica](https://subnautica.thunderstore.io/package/BepInEx/BepInExPack_Subnautica/)** (v5.4.21+).
2. Install **[Nautilus](https://subnautica.thunderstore.io/package/SubnauticaModding/Nautilus/)** (v1.0.0+).
3. Download or build `CallOfTheAbyss.dll`.
4. Copy `CallOfTheAbyss.dll` and `mod.json` into:
   ```
   Subnautica/BepInEx/plugins/CallOfTheAbyss/
   ```
5. Launch Subnautica and dive into the depths!

---

## 💻 Building from Source

Requirements:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/)

```bash
# Clone the repository
git clone https://github.com/Lunar-Entertainment/SubMod.git
cd SubMod

# Restore packages & build
dotnet restore
dotnet build -c Release
```

The compiled plugin will be output to `bin/Release/CallOfTheAbyss.dll`.

---

## 📜 License
Licensed under the [MIT License](LICENSE).
