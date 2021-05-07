Nekoliko dodatnih stvari:
- Fakultet i Student klase su one s kojima bi krajnji korisnik radio (možda bih ID stavio kao nekakav invisible property)
- Entity klase odgovaraju tablicama u bazi
- Input klase prestavljaju format podataka za Post i Put

Znam da Post i Put za Student.FakultetID nisu najbolje riješeni, ali ideja je na frontend staviti dropdown menu preko kojeg odabiremo fakultet te se onda automatski doda FakultetID u .json (ne unosi ga krajnji korisnik).<br>

Ovakve stvari sam ja na projektu iz kolegija Baze podataka riješio pomoću procedura koje bi primile naziv predmeta, pronašle njegov ID i onda taj ID koristile za unos (baza je bila dizajnirana tako da se apsolutno sve radi preko procedura), ali ovdje nisam htio još dodatno komplicirati stvari na repozitoriju.