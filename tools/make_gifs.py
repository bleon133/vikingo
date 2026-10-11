"""
Genera GIFs animados a partir de las hojas de sprites del proyecto Vikingo.
Uso: python tools/make_gifs.py
Salida: docs/img/gifs/*.gif
"""
import os
from PIL import Image

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
ASSETS = os.path.join(ROOT, "Vikingo", "Assets")
OUT = os.path.join(ROOT, "docs", "img", "gifs")

FRAME = 64          # todos los frames del proyecto son de 64x64
SCALE = 3           # ampliacion para que se vean bien en Markdown
DURATION = 110      # ms por frame
TRANSPARENT = (0, 0, 0, 0)


def frames_from_sheet(path, frame_w=FRAME, frame_h=FRAME):
    """Recorta una hoja horizontal en frames cuadrados."""
    img = Image.open(path).convert("RGBA")
    w, h = img.size
    n = w // frame_w
    out = []
    for i in range(n):
        box = (i * frame_w, 0, (i + 1) * frame_w, frame_h)
        out.append(img.crop(box))
    return out


def strip_empty(frames):
    """Quita frames completamente vacios (hojas con padding)."""
    keep = []
    for f in frames:
        bbox = f.getbbox()
        if bbox is None:
            continue
        keep.append(f)
    return keep or frames


def compose_horizontal(groups, frame_w=FRAME, frame_h=FRAME, gap=4):
    """Pega varios grupos de frames uno al lado del otro.
    Devuelve una lista de frames compuestos (uno por paso de animacion)."""
    n = max(len(g) for g in groups)
    total_w = len(groups) * frame_w + (len(groups) - 1) * gap
    out = []
    for x in range(n):
        sheet = Image.new("RGBA", (total_w, frame_h), TRANSPARENT)
        cursor = 0
        for g in groups:
            f = g[min(x, len(g) - 1)]
            sheet.paste(f, (cursor, 0), f)
            cursor += frame_w + gap
        out.append(sheet)
    return out


def save_gif(frames, name, scale=SCALE, duration=DURATION):
    os.makedirs(OUT, exist_ok=True)
    big = []
    for f in frames:
        big.append(f.resize((f.width * scale, f.height * scale), Image.NEAREST))
    # Fondo opaco para que el GIF se vea bien en GitHub (que no soporta alpha en GIF)
    flat = []
    for f in big:
        bg = Image.new("RGBA", f.size, (24, 22, 30, 255))
        bg.alpha_composite(f)
        flat.append(bg.convert("P", palette=Image.ADAPTIVE, colors=255))
    out = os.path.join(OUT, name)
    flat[0].save(out, save_all=True, append_images=flat[1:],
                 duration=duration, loop=0, disposal=2)
    print(f"  {name}  ({len(flat)} frames, {flat[0].width}x{flat[0].height})")


def strip_frames(path):
    return strip_empty(frames_from_sheet(path))


def sprite(*parts):
    return os.path.join(ASSETS, *parts)


def main():
    print("Generando GIFs...")

    v = ("Sprites", "Personajes", "Vikingo")
    e = ("Sprites", "Personajes", "EnemigoCruzado")
    n = ("Sprites", "Personajes", "Npc Vikingo")

    # --- Hakon: caminar en 4 direcciones combinado ---
    walk = compose_horizontal([
        strip_frames(sprite(*v, "Vikingo_Caminar_Abajo.png")),
        strip_frames(sprite(*v, "Vikingo_Caminar_Arriba.png")),
        strip_frames(sprite(*v, "Vikingo_Caminar_Derecha.png")),
        strip_frames(sprite(*v, "Vikingo_Caminar_Izquierda.png")),
    ])
    save_gif(walk, "hakon-caminar.gif")
    save_gif(strip_frames(sprite(*v, "Vikingo_Caminar_Abajo.png")), "hakon-caminar-abajo.gif")
    save_gif(strip_frames(sprite(*v, "Vikingo_Idle_Abajo.png")), "hakon-idle.gif")
    save_gif(strip_frames(sprite(*v, "Vikingo_Muerte.png")), "hakon-muerte.gif", duration=150)

    # --- Enemigos ---
    inf = ("Enemigo Infanteria",)
    alt = ("Enemigo Rango Alto",)
    save_gif(compose_horizontal([
        strip_frames(sprite(*e, *inf, "PJ-Prototipo-3animacion Inferior.png")),
        strip_frames(sprite(*e, *inf, "PJ-Prototipo-3animacion Superior.png")),
    ]), "enemigo-infanteria-caminar.gif")
    save_gif(strip_frames(sprite(*e, *inf, "PJ-Prototipo-3animacion Inferior Aatque enemigo.png")),
             "enemigo-infanteria-ataque.gif", duration=90)
    save_gif(compose_horizontal([
        strip_frames(sprite(*e, *alt, "PJ-Prototipo-3animacion Inferior.png")),
        strip_frames(sprite(*e, *alt, "PJ-Prototipo-3animacion Superior.png")),
    ]), "enemigo-caballero-caminar.gif")
    save_gif(strip_frames(sprite(*e, *alt, "PJ-Prototipo-3animacion Inferior Aatque enemigo.png")),
             "enemigo-caballero-ataque.gif", duration=90)

    # --- NPCs ---
    npcs = [
        ("npc-freya.gif", [ "Taberna", "Mujer_Taberna_Idle_Abajo.png" ]),
        ("npc-sabio.gif", [ "Sabio", "sabio_idle.png" ]),
        ("npc-herrero.gif", [ "Herrero", "Hombre_Herrero_Idle.png" ]),
        ("npc-herrero-forja.gif", [ "Herrero", "herrero forja.png" ]),
        ("npc-intro.gif", [ "Aldeana_Inicio", "Aldeana_Inicio_Idle.png" ]),
        ("npc-huerto.gif", [ "Huerto", "Huerto.png" ]),
        ("npc-cabana.gif", [ "Caba\u00f1a", "Caba\u00f1a.png" ]),
        ("npc-entrenamiento.gif", [ "Entrenamiento", "CampoEntrenamiento.png" ]),
        ("npc-taberna-bebiendo.gif", [ "Taberna", "aldeano_bebiendo.png" ]),
    ]
    for name, parts in npcs:
        p = sprite(*n, *parts)
        if os.path.exists(p):
            save_gif(strip_frames(p), name, duration=140)
        else:
            print(f"  (falta) {parts}")

    # --- Enemigo arana (cueva) ---
    ar = sprite("Sprites", "Mapa Vikingo", "Cueva", "Ara\u00f1a", "ara\u00f1as - copia.png")
    if os.path.exists(ar):
        img = Image.open(ar).convert("RGBA")
        save_gif(strip_empty(frames_from_sheet(ar, img.height, img.height)), "enemigo-arana.gif")

    print("Listo.")


if __name__ == "__main__":
    main()

