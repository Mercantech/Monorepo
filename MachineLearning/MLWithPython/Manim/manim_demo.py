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
