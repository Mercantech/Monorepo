"""
Manim Demo – matematik-animationer med Manim Community.

Kør f.eks.:
  manim -pql manim_demo.py IntroScene
  manim -pqh manim_demo.py CircleEquation
  manim -pql manim_demo.py EulersIdentity
  manim -pql manim_demo.py Pythagoras
"""

from manim import *


class IntroScene(Scene):
    """Simpel intro med titel og undertekst."""

    def construct(self):
        title = Text("Manim Demo", font_size=72, color=BLUE)
        subtitle = Text(
            "Matematik-animationer med Python",
            font_size=36,
            color=GRAY,
        ).next_to(title, DOWN)
        self.play(Write(title), run_time=1.5)
        self.play(FadeIn(subtitle), run_time=1)
        self.wait(1)
        self.play(FadeOut(title), FadeOut(subtitle))


class CircleEquation(Scene):
    """Cirkel og ligningen x² + y² = r²."""

    def construct(self):
        circle = Circle(radius=2, color=BLUE)
        equation = Text("x² + y² = r²", font_size=48).to_edge(DOWN)
        self.play(Create(circle), run_time=2)
        self.play(Write(equation), run_time=1.5)
        self.wait(2)


class EulersIdentity(Scene):
    """Eulers identitet: e^(iπ) + 1 = 0."""

    def construct(self):
        eq = Text("e^(iπ) + 1 = 0", font_size=56, color=YELLOW)
        self.play(Write(eq), run_time=2)
        self.wait(2)
        self.play(eq.animate.scale(1.2).set_color(GOLD), run_time=1)
        self.wait(1)


class Pythagoras(Scene):
    """Pythagoras' sætning: a² + b² = c² med en retvinklet trekant."""

    def construct(self):
        # Retvinklet trekant (kateter 2 og 1.5)
        triangle = Polygon(
            [0, 0, 0],
            [2, 0, 0],
            [2, 1.5, 0],
            color=WHITE,
        )
        triangle.move_to(ORIGIN).shift(LEFT * 1.5)
        a_label = Text("a", font_size=32).next_to(
            triangle.get_bottom(), DOWN, buff=0.2
        )
        b_label = Text("b", font_size=32).next_to(
            triangle.get_right(), RIGHT, buff=0.2
        )
        c_label = Text("c", font_size=32).next_to(
            triangle.get_top(), UP + LEFT, buff=0.2
        )
        equation = Text("a² + b² = c²", font_size=44).to_edge(RIGHT)

        self.play(Create(triangle), run_time=1.5)
        self.play(FadeIn(a_label), FadeIn(b_label), FadeIn(c_label), run_time=0.8)
        self.play(Write(equation), run_time=1.5)
        self.wait(2)


class GraphDemo(Scene):
    """Simpel parabel y = x²."""

    def construct(self):
        # Uden include_numbers – aksetal bruger LaTeX og kræver TeX
        axes = Axes(
            x_range=[-3, 3, 1],
            y_range=[0, 9, 2],
            x_length=6,
            y_length=5,
            axis_config={"include_numbers": False},
        )
        # Tilføj tal som Text (virker uden LaTeX)
        x_labels = VGroup(
            *[Text(str(n), font_size=24).next_to(axes.c2p(n, 0), DOWN) for n in [-2, 0, 2]]
        )
        y_labels = VGroup(
            *[Text(str(n), font_size=24).next_to(axes.c2p(0, n), LEFT) for n in [2, 4, 6, 8]]
        )
        graph = axes.plot(lambda x: x**2, color=GREEN)
        label = Text("y = x²", font_size=36).next_to(
            graph.point_from_proportion(0.7), RIGHT, buff=0.3
        )
        self.play(Create(axes), run_time=1)
        self.play(FadeIn(x_labels), FadeIn(y_labels), run_time=0.5)
        self.play(Create(graph), run_time=2)
        self.play(Write(label), run_time=1)
        self.wait(2)


# --- Statistik: stolpediagram ---


class StolpediagramDemo(Scene):
    """Fra data til stolpediagram: viser trin for trin hvordan man laver en graf."""

    def construct(self):
        title = Text("Stolpediagram", font_size=56, color=BLUE)
        title.to_edge(UP)
        self.play(Write(title), run_time=1)
        self.wait(0.5)

        # 1) Vis data som tekst
        data_title = Text("Data", font_size=36, color=YELLOW).next_to(title, DOWN, buff=0.6)
        self.play(FadeIn(data_title), run_time=0.5)
        # Eksempel: Frugt og antal solgt
        kategorier = ["Æbler", "Pærer", "Bananer", "Appelsiner"]
        værdier = [4, 7, 3, 5]
        data_lines = VGroup(
            Text("Æbler: 4    Pærer: 7    Bananer: 3    Appelsiner: 5", font_size=28),
        ).next_to(data_title, DOWN, buff=0.3)
        self.play(Write(data_lines), run_time=1.5)
        self.wait(1)

        # 2) Transformér til "Nu laver vi en graf"
        self.play(
            FadeOut(data_title),
            FadeOut(data_lines),
            title.animate.scale(0.9).move_to(ORIGIN).shift(UP * 2.8),
            run_time=0.8,
        )
        step_text = Text("Trin 1: Tegn akser", font_size=32, color=GRAY).to_edge(DOWN, buff=0.4)
        self.play(FadeIn(step_text), run_time=0.4)

        # Akser (simpel tallinje + lodret akse)
        y_axis = Line(ORIGIN, UP * 2.2, color=WHITE).shift(LEFT * 2.5)
        x_axis = Line(LEFT * 2.5, RIGHT * 2.5, color=WHITE).shift(DOWN * 1.2)
        self.play(Create(y_axis), Create(x_axis), run_time=1)
        self.wait(0.5)

        # Y-aksen tal 0-8 (placeret ved højden i)
        y_labels = VGroup(
            *[Text(str(i), font_size=22).next_to(y_axis.get_start() + UP * (i * 0.275), LEFT, buff=0.15) for i in [0, 2, 4, 6, 8]]
        )
        self.play(FadeIn(y_labels), run_time=0.5)
        self.play(FadeOut(step_text), run_time=0.3)

        step_text2 = Text("Trin 2: Søjler for hver kategori", font_size=32, color=GRAY).to_edge(DOWN, buff=0.4)
        self.play(FadeIn(step_text2), run_time=0.4)

        # Søjler (stolper) – bredde og højde skaleret
        bar_width = 0.35
        bar_scale = 0.22  # 1 enhed = 0.22 manim-enheder
        colors = [BLUE, GREEN, YELLOW, MAROON]
        bars = VGroup()
        x_start = -1.8
        for i, v in enumerate(værdier):
            h = v * bar_scale
            rect = Rectangle(
                width=bar_width,
                height=h,
                fill_color=colors[i],
                fill_opacity=0.8,
                stroke_color=WHITE,
                stroke_width=1,
            )
            rect.move_to(x_axis.get_center() + RIGHT * (x_start + i * 1.1) + UP * (h / 2))
            bars.add(rect)

        for bar in bars:
            self.play(GrowFromEdge(bar, DOWN), run_time=0.6)
        self.wait(0.5)
        self.play(FadeOut(step_text2), run_time=0.3)

        # Kategorier under x-aksen
        cat_labels = VGroup(
            Text("Æbler", font_size=20).next_to(bars[0], DOWN, buff=0.15),
            Text("Pærer", font_size=20).next_to(bars[1], DOWN, buff=0.15),
            Text("Bananer", font_size=20).next_to(bars[2], DOWN, buff=0.15),
            Text("Appelsiner", font_size=20).next_to(bars[3], DOWN, buff=0.15),
        )
        self.play(FadeIn(cat_labels), run_time=0.8)

        # Titel på y-aksen
        y_axis_label = Text("Antal", font_size=24, color=GRAY).next_to(y_axis, LEFT, buff=0.3)
        self.play(FadeIn(y_axis_label), run_time=0.4)
        self.wait(2)

        # Afslutning
        slut = Text("Stolpediagrammet viser dataene som søjler", font_size=28, color=GRAY).to_edge(DOWN, buff=0.5)
        self.play(FadeIn(slut), run_time=0.6)
        self.wait(2)
