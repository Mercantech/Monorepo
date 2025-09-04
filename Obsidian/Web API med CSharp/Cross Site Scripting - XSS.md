Cross Site Scripting, også kaldet XSS, er ulovligt, så man skal ikke prøve det på sider andre end ens egne

Cross Site Scripting er hvor man skriver ondsindet kode, i form af JavaScript, på en offentlig hjemmeside som har et ikke-valideret inputfelt eller tekstboks. Dette kan få browseren til at køre koden som man har skrevet, endda på andres computer.

Et eksempel på en XSS som spredte sig hurtigt var [Samy](https://en.wikipedia.org/wiki/Samy_\(computer_worm\)).

Det var en kodeblock som blev skrevet ind på MySpace som ændrede brugerens ens profils beskrivelse og sendte en venneanmodning til en profil som hed Samy. Relativt harmløs virus/XSS, men den nåede at sprede sig hurtig. Koden kan findes her: [https://samy.pl/myspace/tech.html](https://samy.pl/myspace/tech.html)

Der er lavet en short-doc omkring det, som man kan se her:

[Greatest Moments In Hacking History: Samy Kamkar Takes Down Myspace](https://video.vice.com/en_us/video/samy-kamkar/56967e5eebd057947f8d0fc5)

Siden er ofte nede, men her er videon på yt - [https://www.youtube.com/watch?v=9yOjLV5wlIo](https://www.youtube.com/watch?v=9yOjLV5wlIo)

En rigtig fin forklaring på problemet og hvordan det virker teknisk er her og pensum.
[Cracking Websites with Cross Site Scripting - Computerphile](https://youtu.be/L5l9lSnNMxg)