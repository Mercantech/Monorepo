# Active Directory – Roller, Grupper og RDP-adgang

I denne guide viser vi, hvordan man kan styre **adgang via grupper** i Active Directory.  
Eksemplet er en receptionist (Rasmus), som skal have adgang til at logge ind via Remote Desktop og arbejde i en delt mappe.

---

## 1. Opret en testbruger
1. Åbn **Active Directory Users and Computers (ADUC)**.
2. Opret en ny bruger:
   - **First name:** Rasmus  
   - **Last name:** Receptionist  
   - **User logon name:** `Rasmus`  
   - Sæt et password, og fjern kravet om at ændre ved første login (så det er nemt at teste).

---

## 2. Opret en global gruppe til receptionen
1. I ADUC → højreklik på din OU → **New → Group**.  
2. Navngiv gruppen fx `Reception`.  
3. Vælg:  
   - **Group scope:** Global  
   - **Group type:** Security  
4. Tilføj Rasmus som medlem af gruppen.

---

## 3. Opret en Domain Local gruppe til RDP
For at tildele adgang til ressourcer skal vi bruge en **Domain Local Group**.

1. I ADUC → højreklik → **New → Group**.  
2. Navngiv den fx `Reception-RDP-Access`.  
3. Vælg:  
   - **Group scope:** Domain Local  
   - **Group type:** Security  
4. Tilføj `Reception` (den globale gruppe) som medlem.

---

## 4. Tilføj Domain Local gruppen til *Remote Desktop Users*
1. Gå til OU **Builtin** i ADUC.  
2. Find gruppen **Remote Desktop Users**.  
3. Dobbeltklik → fanen **Members**.  
4. Klik **Add** og tilføj `Reception-RDP-Access`.  

👉 Nu har alle i `Reception` indirekte adgang til RDP, via Domain Local gruppen.

---

## 5. Tildel rettigheder i Local Security Policy
1. På serveren → åbn **secpol.msc**.  
2. Naviger til:  
	Security Settings → Local Policies → User Rights Assignment

3. Find **Allow log on through Remote Desktop Services**.  
4. Tilføj gruppen `Reception-RDP-Access`.  

---

## 6. Test login
1. På din klient → åbn Remote Desktop Connection (RDP).  
2. Indtast:  
- **User name:** `demo\Rasmus`  
- **Password:** (det du satte)  
3. Nu kan Rasmus logge på serveren via RDP.

---

## 7. Opsummering
- **Brugere** lægges i en **Global Group** (fx Reception).  
- **Global Group** lægges i en **Domain Local Group** (fx Reception-RDP-Access).  
- **Domain Local Group** får tildelt adgang til ressourcer (fx Remote Desktop Users).  

Dette er et eksempel på **AGDLP-modellen**:  
Accounts → Global → Domain Local → Permissions.

---
